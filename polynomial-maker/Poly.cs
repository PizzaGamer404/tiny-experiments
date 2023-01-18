using System;

public class Poly {
    // useful functions
    public void CopyTo(Poly to) {
        for (int i = 0; i < degree + 1; i++)
            to.values[i] = values[i];
    }

    public void CopyFrom(Poly from) {
        for (int i = 0; i < degree + 1; i++)
            values[i] = from.values[i];
    }

    public void CopyTo(float[] to) {
        for (int i = 0; i < degree + 1; i++)
            to[i] = values[i];
    }

    public void CopyFrom(float[] from) {
        for (int i = 0; i < degree + 1; i++)
            values[i] = from[i];
    }

    // randomizes each point a little
    public void Randomize(float amount, Random rand) {
        for (int i = 0; i < degree + 1; i++)
            values[i] += (float)((rand.NextDouble() - 0.5) * amount / (i+1));
    }
    // same as before but it actually sets the value instead of changing it
    public void SetRandom(float amount, Random rand) {
        for (int i = 0; i < degree + 1; i++) {
            values[i] = (float)((rand.NextDouble() - 0.5) * amount / (i+1));
            
        }
    }
    // training
    public float TrainToPoints((float x, float y)[] points, Random rand, int amount) {
        // this holds the old values
        float[] temp = new float[degree + 1];
        // this is the closest it has gotten
        float bestDiff = AverageDifference(points);
        for (int a = 0; a < amount; a++) {
            // copy the values over to store them
            CopyTo(temp);
            // randomize values
            // based on which itteration it is it will chose a different amount
            // to randomize it by
            if (a % 3125 == 0)
                Randomize(bestDiff * 10, rand);
            else if (a % 625 == 0)
                Randomize(bestDiff, rand);
            else if (a % 125 == 0)
                Randomize(bestDiff / 10, rand);
            else if (a % 25 == 0)
                Randomize(bestDiff / 10000, rand);
            else if (a % 5 == 0)
                Randomize(bestDiff / 10000000, rand);
            else
                Randomize(bestDiff / 10000000000, rand);
            // check the difference
            float diff = AverageDifference(points);
            // if it is worse, revert the numbers from the saved ones
            if (diff > bestDiff)
                CopyFrom(temp);
            // otherwise update the difference
            else
                bestDiff = diff;
        }
        return bestDiff;
    }
    // just calls the train function with the default 5000 itterations
    public float TrainToPoints((float x, float y)[] points, Random rand) => TrainToPoints(points, rand, 5000);

    // generates the equation string
    public string Equation {
        get {
            string[] strs = new string[degree + 1];
            for (int i = 0; i < degree - 1; i++)
                strs[i] = $"{values[i]}x^{degree - i}";
            strs[degree - 1] = $"{values[degree - 1]}x";
            strs[degree] = values[degree].ToString();
            return string.Join(" + ", strs);
        }
    }

    // generates a desmos compatible equation string
    public string Desmos {
        get {
            string[] strs = new string[degree + 1];
            for (int i = 0; i < degree - 1; i++)
                strs[i] = values[i].ToString(".###################") + "x^{" + (degree - i) + "}";
            strs[degree - 1] = values[degree - 1].ToString(".###################") + "x";
            strs[degree] = values[degree].ToString(".###################");
            return string.Join("+", strs);
        }
    }

    // the polynomial values
    private float[] values;
    // the polynomial degree
    public readonly uint degree;
    
    public Poly(uint degree) {
        this.degree = degree;
        values = new float[degree + 1];
    }

    // calculates Y given X in the polynomial
    public float Calculate(float x) {
        float value = 0;
        for (int i = 0; i < values.Length - 2; i++) {
            value += MathF.Pow(x, degree - i) * values[i];
        }
        value += x * values[degree - 1];
        value += values[degree];
        return value;
    }

    // finds the average difference between the polynomial's Y values
    // and the desired Y values
    public float AverageDifference(params (float x, float y)[] points) {
        float diff = 0;
        foreach (var p in points)
            diff += MathF.Abs(p.y - Calculate(p.x));
        return diff / points.Length;
    }
}
