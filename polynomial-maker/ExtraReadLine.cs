using System;

// these are just some functions I made to make it easier to get values
// from the user
public static class ExtraReadLine {
    public static int ReadInt() {
        string s = Console.ReadLine();
        int i;
        while (!int.TryParse(s, out i)){
            Console.WriteLine($"Couldn't interpret \"{s}\" as an integer.");
            Console.WriteLine("Please enter an integer.");
            s = Console.ReadLine();
        }
        return i;
    }
    public static int ReadInt(int min) {
        string s = Console.ReadLine();
        int i;
        while (!int.TryParse(s, out i) || i < min){
            Console.WriteLine($"Couldn't interpret \"{s}\" as an integer greater than {min}.");
            Console.WriteLine($"Please enter an integer greater than {min}.");
            s = Console.ReadLine();
        }
        return i;
    }
    public static int ReadInt(int min, int max) {
        string s = Console.ReadLine();
        int i;
        while (!int.TryParse(s, out i) || i < min || i > max){
            Console.WriteLine($"Couldn't interpret \"{s}\" as an integer between {min} and {max}.");
            Console.WriteLine($"Please enter an integer between {min} and {max}.");
            s = Console.ReadLine();
        }
        return i;
    }


    public static float ReadFloat() {
        string s = Console.ReadLine();
        float f;
        while (!float.TryParse(s, out f)){
            Console.WriteLine($"Couldn't interpret \"{s}\" a number.");
            Console.WriteLine("Please enter a number.");
            s = Console.ReadLine();
        }
        return f;
    }
    public static float ReadFloat(float min) {
        string s = Console.ReadLine();
        float f;
        while (!float.TryParse(s, out f) || f < min){
            Console.WriteLine($"Couldn't interpret \"{f}\" as a number greater than {min}.");
            Console.WriteLine($"Please enter a number greater than {min}.");
            s = Console.ReadLine();
        }
        return f;
    }
    public static float ReadFloat(int min, int max) {
        string s = Console.ReadLine();
        float f;
        while (!float.TryParse(s, out f) || f < min || f > max){
            Console.WriteLine($"Couldn't interpret \"{f}\" as a number between {min} and {max}.");
            Console.WriteLine($"Please enter a number between {min} and {max}.");
            s = Console.ReadLine();
        }
        return f;
    }
}
