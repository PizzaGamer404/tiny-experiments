using System;
using static ExtraReadLine;

class Program{
    // the polygons
    private static Poly[] testPolys;
    // the coordinates the polygons try to fit
    private static (float x, float y)[] coords;
    // rand
    private static Random rand;

    // reads the coords from points.txt
    private static void ReadPoints() {
        string text = System.IO.File.ReadAllText("points.txt");
        string[] parts = text.Split('\n');
        coords = new (float x, float y)[parts.Length];
        for (int i = 0; i < parts.Length; i++) {
            string[] xAndY = parts[i].Split(", ");
            coords[i] = new (float.Parse(xAndY[0]), float.Parse(xAndY[1]));
        }
    }
        
    public static void Main(){

        Restart:
        
        // gets all of the data needed
        // a polynomial with a degree of three is ax^3 + bx^2 + cx + d
        Console.WriteLine("What degree should the polynomail have?");
        int degree = ReadInt(1, 100);
        // currently doesn't actually do anything
        // the plan is that if they get within this margine of error it will
        // end early
        Console.WriteLine("What margin of error should be targeted?");
        float margin = ReadFloat(0, 1000);
        // the amount of itteration batches to run on the polynomials
        Console.WriteLine("How many itterations (x5000) can be ran?");
        int itterations = ReadInt(1, 10000);
        // the amount of polygons to use
        // since they kind of go in random directions
        // it might be worth running multiple and hoping one is good
        Console.WriteLine("How many polygons should be used?");
        int polygons = ReadInt(1, 1000);

        // this just gets a random value generator
        rand = new Random(DateTime.Now.GetHashCode());

        // this reads in the coordinates in points.txt
        // you used to have to write in each one but that was painful
        ReadPoints();

        // creates the polygons
        // a new polygon is just a straight line
        // I see how far the initial line is from the target
        // and use that to choose how initially random they are
        float startRand = (new Poly(1)).AverageDifference(coords);
        testPolys = new Poly[polygons];
        for (int i = 0; i < polygons; i++) {
            // here I create the polygon
            testPolys[i] = new Poly((uint)degree);
            // and here I randomize it
            testPolys[i].SetRandom(startRand, rand);
        }

        // for each itteration
        for (int i = 0; i < itterations; i++) {
            // record the closest it gets
            float best = float.MaxValue;
            // runs a batch on each polygon
            for (int j = 0; j < polygons; j++) {
                best = MathF.Min(best, testPolys[j].TrainToPoints(coords, rand));
            }
            // logs how close it was and its progress
            Console.WriteLine($"{(i + 1)}/{itterations} margin: {best}");
        }

        // finds the closest polygon
        Poly bestPoly = testPolys[0];
        float bestValue = testPolys[0].AverageDifference(coords);
        for (int j = 1; j < polygons; j++) {
            float val = testPolys[j].AverageDifference(coords);
            if (val < bestValue) {
                bestValue = val;
                bestPoly = testPolys[j];
            }
        }
        // writes the equation
        Console.WriteLine("\nEquation: " + bestPoly.Equation);
        // writes an equation that can be pasted into desmos
        Console.WriteLine("\nDesmos: " + bestPoly.Desmos);

        // this is really for my debugging
        // it just says what it was going for and what it got at each point
        foreach (var c in coords) {
            Console.WriteLine($"{c.x}: {c.y} {bestPoly.Calculate(c.x)}");
        }

        // might add more options later
        // just lets the user restart without restarting the program
        Console.WriteLine("\nRestart (1) Exit (2)");
        int choice = ReadInt(1, 2);
        if (choice == 1)
            goto Restart;
    }
}
