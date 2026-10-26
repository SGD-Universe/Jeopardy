// Feel free to search this document for what is relevant to you ~DeeFeeCee
using System;                               // Used for so many things; just use it
using System.Collections;                   // Used for IEnumerator, ArrayList, & Hashtable
using System.Collections.Generic;           // Used for List<T> & Dictionary<TKey, TValue>
using System.Diagnostics;                   // Used for Stopwatch: high precision debugging
using System.Linq;
using System.Linq.Expressions;
using System.Text;                          // Used for StringBuilder, ===
using NUnit.Framework.Internal;
using TMPro;                                // Used for rendering rich text in-game
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEditor.Experimental.GraphView;
using UnityEngine;                          // Used for everything Unity
using Debug = UnityEngine.Debug;            // Debug exists in System.Diagnostics & UnityEngine so alias this one
using UnityEngine.UI;
using UnityEditor.TerrainTools;
using UnityEngine.UIElements;
//using static UnityEngine.Debug;           // "static" here lets us access the Debug class directly
/* This lets us simply type Log("Log message") instead of Debug.Log()
This is handy, but for tutorial reasons, I leave this commented */
#region Namespaces
/* The above are often used in this project
Unused "using" lines don't affect anything, so there's no need to remove them
But if by the end of development, you don't use certain namespaces, they may be removed

If not for "using [Name.Space]", we'd have to qualify "Name.Space.Class.Method()" every time
If there would be a collision between namespaces, such as Random() from system & Random.Range() from UnityEngine,
you will need to specify which one, whether System.Random() or UnityEngine.Random.Range()

Namespaces follow the following conventions:
https://learn.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-namespaces
Code that is shared with others should always have a namespace (not applicable for a Unity project)
To use a namespace, follow this structure: */
namespace CentralPiedmont.ExampleGameLogic // Ideally, this file would be in the parent folder of Assets/Scripts
{
    public class GameBoard
    {
        // Code goes here
    }
}
/* -----------------------------------------------------------------------------------

Throughout this script, pay attention to when semicolon (;) is used & when it isn't

Also, if you don't understand something, make a note (such as // ===) & come back to it later

----------------------------------------------------------------------------------- */
#endregion Namespaces
public class CSharpTutorial : MonoBehaviour
/* You'll see "public class [class name] : MonoBehaviour" in virtually every script file for Unity
This is included so we can use Start() & the other Unity methods below,
have this code interact with other components of the GameObject it's attached to,
& make use of game events & coroutines */
{
#region Unity Methods
    void Awake() // Runs once when the script is first loaded, regardless if it's enabled
    {} // Avoid calls to other scripts in Awake

    void OnEnable() // Runs every time the script is enabled
    {} // Avoid calls to other scripts in OnEnable

    void OnDisable() // Runs every time the script is disabled
    {} // Avoid calls to other scripts in OnDisable

    void Start() /* Runs once after Awake() when the script is first loaded, but not if it's disabled
    Can be used to call other scripts */
    {
        GreetPlanet();
        DisplayFirstName();
        WhoMadeThis();
    }

    void FixedUpdate() // Runs at fixed time intervals with physics engine
    {}

    void Update() // Runs once per frame (dependent on frame rate)
    {}
#endregion Unity Methods
#region Comments
    void Comments()
    {
        // Single line comments can be made with a double forward slash
        // It's good practice to place a space after the slashes for true comments
        // & no space for code:
        //ExampleLine(Code);

        // I personally recommend avoiding periods at the end of comments

        Debug.Log("Hi"); // Use a space before the slashes when commenting on the same line as code

        /*
        Block comments can be made between a forward slash followed by an asterisk
        & an asterisk followed by a forward slash
        */

        Debug.Log(""/* We can even use this to put comments inside code, I guess? */);

        #region Region Name
        /* We can create regions in code which are collapsable in dev environments like VSC
        They're mostly useful to navigate long sections which otherwise have no collapsable sections
        I use them here to make it easy to find what you need, but they're not often needed here
        Note: We can also collapse methods, classes, loops, etc. */
        #region 3rd-Level Region
        // We can even put regions inside other regions! This is a region in a region in a region
        #endregion // Every region must be paired with an #endregion after the region we want to define
        // We technically don't need to name which region we're ending
        #endregion Region Name
        // But I do anyway

        /* We can comment out selected text in Visual Studio Code like so:
        Line comment: Ctrl + /
        Block comment: Shft + Alt + A
        */
    }
    // To see the below in VS/VSC, add the parameters into your own method declaration & type "///" directly above
    #region Code Summary
    /// <summary>
    /// <br>This is a broad description of the method</br>
    /// <br>and will show up when you hover over the method name</br>
    /// </summary>
    /// <param name="commentLength">Length of comment in centimeters</param>
    /// <param name="canRead">Is reader literate?</param>
    /// <param name="commentWriter">Name of person responsible</param>
    #endregion
    // The <br> & </br> are just to indicate separate line breaks
    void CommentPro(int commentLength, bool canRead, string commentWriter) // Hover over the method & param names
    // If you can't see any of these descriptions, you might need a better code editor
        => Debug.Log("" + commentLength + canRead + commentWriter); // Just exists to "use" the parameters
#endregion Comments
#region Variables & Constants
    /* Always use descriptive names for variables, constants, methods, classes, etc.
    Avoid single-letter names & abbreviations (except for internal usage, like in a for loop)
    Include unit name in variable name (delaySeconds instead of just delay)
    Don't put types in your type names
    Don't make variable names too similar: It's better to have a longer name than leave readers confused */
    void Variables()
    {
        int myInteger = 1337; // ints range from -2,147,483,648 to 2,147,483,647 (-2^31 to 2^31-1)
        int myLongerInteger = 2_147_483_647; // Numeric types accept underscores as separators
        double myDouble = 1.414213562373095; // Doubles have a precision of ~15-17 digits (8 bytes)
        double sciNotation = 6.022_140_76e23; // Doubles/floats/decimals may be represented with scientific notation
        float myFloat = 3.14159265F; /* Floats have a precision of ~6-9 digits (4 bytes)
        Note the "F" at the end of the float value */
        decimal myDecimal = 23.14069263277926900572908636M;
        /* Decimals have a precision of 28-29 digits (16 bytes)
        Note the "M" at the end
        Use decimals for financial calculations, not floats or doubles */
        decimal myPrice = 1025.96M;
        Debug.Log($"My price: {myPrice:c}"); /* :c puts a currency symbol in front of the decimal
        Prints: "My price: $1,025.69" with en-US CultureInfo */
        bool myBoolean = true; // Can be "true" or "false", without quotation marks
        bool myEqualityTest = myInteger == myLongerInteger; /* 1337 ≠ 2^31-1, so myEqualityTest is "false"
        Booleans can be set to the state of (in)equalities */

// Strings
        string myString = "This is a string"; // This is what's called initializing a variable
        string myLongerString = "When you want \"quotation marks\", use \\ to escape!";
        Debug.Log(myLongerString); // Prints: When you want "quotation marks", use \ to escape!
        string myAddress = @"C:\School\WBL\Jeopardy\Assets\Scripts";
        /* Particularly for strings with backslash, we can use @ before the quotation mark
        To use quotation marks with this verbatim string notation, use double-double quotes: "" */
        Debug.Log("This: \"" + myString + "\" is a string."); /* Prints: This: "This is a string" is a string.
        The above is one way of including variables along with strings in, for example, a debug message
        Note that Debug.Log (or Console.WriteLine) handles converting types,
        so we can use most variables this way */
        Debug.Log($"This: \"{myString}\" is a string."); /* Prints same as above, usually shorter code to type
        $ before a string allows us to include variable names within {},
        avoiding opening & closing quotes in between */
        string addressingAddress = $"This is this file's location: {myAddress}";
        // String interpolation with $ can be done with normal strings
        Debug.Log(addressingAddress); // Prints: This is this file's location: C:\School\WBL\Jeopardy\Assets\Scripts

        // New line for a breather
        string stringWithTabAndNewLine = "1)\tFirst line\n2\tSecond line)";
        Debug.Log(stringWithTabAndNewLine); /* Prints:
        1)  First line
        2)  Second line
        See the "Brief StringBuilder Tangent" section in the "Methods" region for "modifying" strings */

// Arrays
        string[] myFriendArray = {"Adam", "Dylan", "Amber", "Carlie"}; // String array
        int[] myIntegerArray = {2, 7, 1, 8, 2, 8, 1, 8}; // Integer array
        // We can also populate an array like this if we know the length but not the contents
        string[] myDogsArray = new string[3];
        myDogsArray[0] ="Frederick"; // The "first" element in an array is always 0
        myDogsArray[1] ="Ella Christina";
        myDogsArray[2] ="Jolly";
        // Similarly, you can extract an element from an array like this
        Debug.Log(myDogsArray[1]); // Prints: Ella Christina
        Debug.Log(myDogsArray.Length); /* Prints 3, the number of elements in the array, not the highest value (2)
        Arrays are best for fixed-order lists & not really to be modified
        Whereas lists, which we'll see later, are better suited for modification */

// Back to the other data types
        // The following are all valid types, most of which you probably won't need
        sbyte mySignedByte = -128; // sbytes range from -128 to 127 (-2^7 to 2^7-1)
        byte myUnsignedByte = 255; // bytes range from 0 to 255 (0 to 2^8-1)
        short mySignedShort = -32768; // shorts range from -32,768 to 32,767 (-2^15 to 2^15-1)
        ushort myUnsignedShort = 65535; // ushorts range from 0 to 65,536 (0 to 2^16-1)
        uint myUnsignedInteger = 4_294_967_295; // uints range from 0 to 4,294,967,295 (0 to 2^32-1)
        long mySignedLong = -9_223_372_036_854_775_808;
        // longs range from -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807 (-2^63 to 2^63-1)
        ulong myUnsignedLong = 18_446_744_073_709_551_615;
        // ulongs range from 0 to 18,446,744,073,709,551,615 (0 to 2^64-1)
        char myAsciiCharacter = 'S'; // char accepts any single character we can type
        char myUnicodeCharacter = '\u232C'; // Unicode characters ranges from '\u0000' to '\uFFFF'
        DateTime myDate = DateTime.Now;

        /* We can also introduce a variable by using "var"
        The compiler will figure it out for us
        However, if the type isn't clear from the right-hand side,
        or isn't using the "new" operator,
        then just use the actual type name */
        var anotherString = "Oh boy, here we go again!";
        var anotherInteger = 2026;
        var anotherFloat = -0.618033988f;
        var anotherBoolean = false;
        var anotherDouble = 0.577215664901532D;
        // "D" is technically not needed, since C# assumes decimal values are doubles
        var anotherDate = DateTime.Now;

        // We can introduce multiple variables of the same type together, but this may easily be glossed over
        int integer1 = 10, integer2 = 17;

// Tuples
        var campusLocation = (Latitude: 5.215543f, Longitude: -80.829497f); // var = (float, float)
        //(float Latitude, float Longitude) campusLocal = (5.215543f, -80.829497f);
        // This is another way to initialize it
        Debug.Log($"Location: {campusLocation.Latitude}, {campusLocation.Longitude}");
        /* Prints: Location: 5.215543, -80.829497
        Tuples with named elements can be accessed with variableName.ElementName */

        var campusLocalNameless = (5.215543f, -80.829497f); // This way drops the element names
        Debug.Log($"Location: {campusLocalNameless.Item1}, {campusLocalNameless.Item2}");
        // Tuples with unnamed elements can be accessed using Item1, etc.
        var (laty, longy) = campusLocalNameless; // This allows us to deconstruct the tuple into separate variables
        Debug.Log($"The laty is {laty} & the longy is {longy}");
        // Note how we no longer directly rely on campusLocalNameless

        var (Latitude, Longitude) = (5.215543f, -80.829497f); // This way drops the variable name
        var (_, meridian) = (Latitude, Longitude); /* Deconstructs (extracts) only the Latitude component
        Underscore represents discarded data */
        Debug.Log("Degrees east: " + meridian);

        var latitude = 5.215543f;
        var longitude = -80.829497f;
        var coordinates = (latitude, longitude); // We can initialize tuples like this, too
        Debug.Log($"Location: {coordinates.latitude}, {coordinates.longitude}");
        // Accessed through original var names

// List
        List<int> alpacaHeights = new() { 33, 38, 37, 35 }; /* List of integers
        List<T> is a generic collection that accepts any other type, where T stands for type */
        static (int minimum, int maximum, double average) ComputeAsList(List<int> nums)
        // "nums" represents the name of the list
        {
            var min = nums.Min(); // Finds minimum of list
            var max = nums.Max(); // Finds maximum of list
            var avg = nums.Average(); // Finds arithmetic mean of list
            return (min, max, avg); // ComputeAsList() returns 3 values, the min, max, & avg, for any length list
        }
        var (_, _, mean) = ComputeAsList(alpacaHeights); // Deconstruct only average of list
        Debug.Log($"The average alpaca height is: {mean}");
        // Prints: The average alpaca height is: 35.75

        List<(string Student, int Score)> testResults = new() // List of tuples
        {
            ("Alice", 92),
            ("Bob", 87),
            ("Carol", 95),
            ("Daniel", 100),
            ("Ethan", 75)
        };
        foreach (var (name, grade) in testResults)
        // Tuples can also be deconstructed using foreach, more on foreach later
        {
            Debug.Log($"{name}: {grade}"); // This will print Alice: 92, etc.
        }

        testResults.Add(("Fabio", 86)); // We can add objects to a list
        bool isAliceRemoved = testResults.Remove(("Alice", 92)); /* We can also remove from a list
        Additionally, Remove() outputs true when successful */

        // Note that these aren't the only built-in methods that operate on lists

// Dictionary
        Dictionary<int, string> racePlacement = new() // Format: [key] = value
        //var racePlacement = new Dictionary<int, string> // Another way to initialize a dictionary
        {
            [1] = "The Order",
            [2] = "Selene",
            [3] = "Morgan Myst",
            [5] = "Jonesy", // We don't need to include consecutive keys for the int type
            [7] = "Geno",
            [8] = "Peely"
        };
        string FindByPlacement(int placement)
        {
            string contestant = "Contestant"; // Initializing contestant early in case try() fails
            try
            {
                racePlacement.TryGetValue(placement, out contestant);
                /* Extracts "contestant" as the value corresponding to the "placement" key
                In my case, I'll plug in the int "1" into FindByPlacement & it will return the string "The Order" */
            }
            catch (KeyNotFoundException) // System exception in case of invalid key
            {
                Debug.Log($"No data for placement: {placement}.");
            }
            return contestant;
        }
        Debug.Log($"The winner is {FindByPlacement(1)}"); // Prints: The winner is The Order

        racePlacement.Add(4, "Hope"); // Like a list, we can add to a Dictionary
        Debug.Log(racePlacement[4].ToString()); /* Asks what value is assigned to key 4, then converts to string
        Prints: Hope
        */

        // Dictionary values (& even keys) can be tuples
        Dictionary<string, (int LowEnd, int HighEnd)> sizeOfWords = new() // String key, tuple value
        {
            ["Couple"]  = (2, 3),
            ["Few"]     = (3, 5),
            ["Several"] = (5, 7)
        };
        if (sizeOfWords.TryGetValue("Several", out var range))
        /* Like above, tests whether the key exists, then extracts range to use below, which remember,
        is a tuple, & must be deconstructed to LowEnd & HighEnd */
        {
            Debug.Log($"Several: {range.LowEnd}–{range.HighEnd}"); // Prints: Several: 5-7
        }

        // Note that these aren't the only built-in methods that operate on dictionaries

// Enumerations & HashSets are further down, after the "Loops & Logic" region

// Null & Default Values
        string favoriteBug = null; // We can assign strings directly to null (strings are reference types)
        int? mostFishCaught = null; // Other data types must have the type followed by a question mark
        void DisplayDefaultOf<T>() // "Any type" is often represented simply as "T"
        {
            var val = default(T); // default(type) returns the default value of the type
            //T val = default; // Another way to do it,
            // where T is the same name as within the angle brackets of the method
            Debug.Log($"Default value of {typeof(T)} is {(val is null ? "null" : val.ToString())}");
            // Don't worry about the above if you're just learning C#. Just see the results below
        }
        DisplayDefaultOf<string>(); // Default value of System.String is null
        DisplayDefaultOf<int>(); // Default value of System.Int32 is 0
        DisplayDefaultOf<bool>(); // Default value of System.Boolean is false
        DisplayDefaultOf<DateTime>(); // Default value of System.DateTime is 1/1/0001 12:00:00 AM
        /* Other important defaults:
        Integral types (including int): 0
        float: 0.0f
        double: 0.0d
        decimal: 0m
        char: \x0000
        Any reference type (including string): null */

// Appease Compiler
        /* With type "object", a List doesn't care about data types
        I use it here to get rid of the "The variable 'x' is assigned but its value is never used"
        warning that VSC & Unity sometimes display. We'll see more of this later; ignore the other instances */
        List<object> gibberish = new()
        { myEqualityTest, myDouble, sciNotation, myFloat, myDecimal, myBoolean, myFriendArray, myIntegerArray, mySignedByte,
        myUnsignedByte, mySignedShort, myUnsignedShort, myUnsignedInteger, mySignedLong, myUnsignedLong,
        myAsciiCharacter, myUnicodeCharacter, myDate, anotherString, anotherInteger, anotherFloat, anotherBoolean,
        anotherDouble, anotherDate, integer1, integer2, isAliceRemoved, favoriteBug, mostFishCaught };
        if (gibberish is null) Debug.Log("Gibberish is null");
    }

    void Constants() // Immutable, meaning they cannot be changed
    {
        const int DouglasAdamsNumber = 42; // Constant names are always in PascalCase
        const double FAVORITE_NUMBER = 1.6180339887d; // They can also be ALL CAPS with underscores
        //AdamsNumber = 69; // This code is invalid, as constants are immutable

        List<object> gibberish = new() { DouglasAdamsNumber, FAVORITE_NUMBER };
        if (gibberish is null) Debug.Log("Gibberish is null");
    }
#endregion Variables & Constants
#region Loops & Logic
#region Operators & Preliminary Logic
    void ExampleAssignmentsAndStatements()
    {
        int apples = 10; // Assigns value to variable
        int oranges = 3;
        Debug.Log(apples + oranges); // addition (13)
        Debug.Log(apples - oranges); // subtraction (7)
        Debug.Log(apples * oranges); // multiplication (30)
        Debug.Log(apples / oranges); // division; int truncates toward zero (3)
        Debug.Log(apples % oranges); // remainder when divided (1)

        int funNumber = 42; // Initial variable assignment
        funNumber = 7; // Reassignment of variable
        funNumber = funNumber + 3; // Adds 3 to variable, repeating variable name (10)
        funNumber += 5; // Adds 5 to variable without repeating variable name (15)
        funNumber -= 3; // Subtracts 3 from variable (12)
        funNumber /= 2; // Divides variable by 2 (6)
        funNumber *= 40; // Multiplies variable by 40 (240)
        funNumber %= 22; // Finds remainder when divided by 22 (20)

        int evilNumber = -funNumber; // Assigns evilNumber to be negative of funNumber (-24)
        if (funNumber == 20) // Tests whether x is 20
            Debug.Log("x is 20");
        if (funNumber != 20) // Tests whether x is NOT 20
            Debug.Log("x isn't 20");
        if (funNumber > 19) // Tests whether x is greater than 19 (less than: <)
            Debug.Log("x is larger than 19");
        if (funNumber <= 21) // Tests whether x is less than/equal to 21 (greater/equal to: >=)
            Debug.Log("x is 21 or smaller!");
        if (funNumber == 8 && evilNumber == 8) // && is and, evaluates true if both are true
            Debug.Log("Fun number & evil number are both 8");
        if (funNumber == 12 || evilNumber == 12) // || is or, evaluates true if either are true
            Debug.Log("Fun number, evil number, or both is/are 12");
        /* && (and) & || (or) are known as short-circuiting. For &&, if the first expression is false,
        the rest of the expressions are ignored. For ||, if the first expression is true, the rest
        of the expressions are ignored. See the following examples */
        if (funNumber == evilNumber && funNumber / 0 == 3)
            Debug.Log("The fun number equals the evil number & infinity somehow equals 3");
        // Division by 0 is never evaluated since first expression is false & both must be true
        if (funNumber == 20 || funNumber / 0 == 3)
            Debug.Log("The fun number is 20 or infinity somehow equals 3");
         // Division by 0 is never evaluated since first expression is true & only one must be true

        bool shouldNotCompareThese = apples == oranges; // This is assigned "false", since 10 ≠ 3
        Debug.Log(!shouldNotCompareThese); // "!" prints opposite Boolean value—in this case, true
        bool everythingAsPlanned = apples == 10 && funNumber >= evilNumber && !shouldNotCompareThese;
        Debug.Log(everythingAsPlanned); /* Prints "true", since each statement above returns "true"
        Note that we don't need to say shouldNotCompareThese == false or shouldNotCompareThese != true */

        int mathProblem1 = 8 - 2 * 3 + 7; // C# follows order of operations, so this is 9
        int mathProblem2 = 8 - (2 * 3) + 7; // However, it's best to make code clear for humans
        int mathPart1 = 2 * 3;
        int mathPart2 = 8;
        int mathPart3 = 7;
        int mathProblem3 = mathPart1 + mathPart2 + mathPart3; // When in doubt, break things apart

        float floatNumber = (float) funNumber; // We can convert numeric types between each other
        int bigNumber = 8_675_309;
        byte truncatedNumber = (byte) bigNumber; /* However, note that values may be cut off
        As a result, the value of truncatedNumber is only 64, the maximum possible byte value */
        
        string firstPart = "We finish each other's";
        string secondPart = "sandwiches!";
        string fullPhrase = firstPart + " " + secondPart; /* We can also add, or concatenate, strings
        Result: "We finish each other's sandwiches!"
        Note that a string containing a space was added so that the strings wouldn't end up "likethis" */

        List<object> gibberish = new()
        { mathProblem1, mathProblem2, mathProblem3, floatNumber, truncatedNumber, fullPhrase };
        if (gibberish is null) Debug.Log("Gibberish is null");
    }

    double initialValue = 10;
    double divisor = 0;
    int pitch = 100;
    void ConditionalOperator() // Conditional operator, also known as ternary operator
    {
        // Format: condition ? valueWhenTrue : valueWhenFalse
        string pitchRange = pitch < 250 ? "bass" : "higher than bass";
        /* The logic here is this:
        Set pitchRange to one of these:
        If pitch < 250, then set it to "bass"
        Otherwise, set it to "higher than bass"
        */
        Debug.Log(pitchRange); // Prints "bass"

        // Another example of conditional operator usage
        double safeQuotient = divisor == 0 ? -1 : initialValue / divisor;
        Debug.Log("The result is: " + safeQuotient);
        /* If the divisor is 0, set safeQuotient to -1
        (Assuming the initialValue is always positive, this acts as a flag or failsafe)
        Otherwise, divide initialValue by the divisor */
    }

    void TryAndCatchDivideByZero() // Another way to avoid dividing by zero
    {
        try
        {
            double safeQuotient = initialValue / divisor; // Attempt to divide by zero
            Debug.Log("The answer is: " + safeQuotient);
        }
        catch (DivideByZeroException) // System exception in case of division by zero
        {
            Debug.Log("Cannot divide by zero!");
        }
        /* This will always try to divide by zero, fail,
        but in catching the error, will print "Cannot divide by zero!" */
    }

    void NullOperators()
    {
        decimal? moneyInBank = null;
        //int truncatedBalance = (int)moneyInBank; // Since moneyInBank is null, this is invalid code
        decimal actualMoneyInBank = moneyInBank ?? 0; /* Null-coalescing operator (??)
        This assigns actualMoneyInBank the value of moneyInBank as long as it isn't null
        If it is null, it assigns actualMoneyInBank as 0. Use it to set a default value */
        int truncatedBalance = (int) actualMoneyInBank; /* Space between (int) & actualMoneyInBank isn't necessary
        This is valid since actualMoneyInBank is never null */
        
        int? coinsInPocket = null; // ? after type not strictly necessary for string or string array
        int coinsInPiggyBank = 1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1; // 17
        coinsInPocket ??= coinsInPiggyBank; /* Null-coalescing assignment (??=)
        Assigns coinsInPocket the value of coinsInPiggyBank only if coinsInPocket is null
        Otherwise, the right side of the operation is never evaluated. In this case, it is */
        coinsInPocket ??= coinsInPiggyBank; /* Since coinsInPocket isn't null, this isn't evaluated
        Useful when the right side is computationally expensive */

        string name = null;
        int? lengthOfName = name?.Length; /* Null-conditional member access operator (?.)
        Determines length of string "name", returning null if name is null
        Like (??=), (?.) short-circuits, so since "name" is null, the length is never determined */
        if (lengthOfName != 0)
            Debug.Log("The length of \"name\" is not zero!");
        // Because null is not equal to zero, this log message appears
        if (lengthOfName <= 0) // false
            Debug.Log("The length of name is less than or equal to zero!");
        if (lengthOfName > 0) // false
            Debug.Log("The length of name is greater than zero!");
        // In the previous 2 examples, we see that just because (<=) returns false, doesn't mean (>) returns true

        int[] blankArray = null; // Arrays can handle null values, including the whole thing being null
        int? firstElementInArray = blankArray?[0]; /* Null-conditional indexer access (?[])
        If the array is null, short-circuit. If it's non-null, it returns first element */

        if (firstElementInArray is null) // Use "is null" instead of "== null" except for Unity objects
            Debug.Log("There is no first element in the array, or there is & it's null.");
        if (coinsInPocket is not null) // Use "is not null" instead of "!= null" except for Unity objects
            Debug.Log("I might have some change on hand.");
        // We use "is (not) null" because (==) could be overloaded. "is null" cannot be overloaded

        List<object> gibberish = new() { truncatedBalance };
        if (gibberish is null) Debug.Log("Gibberish is null");
    }
#endregion Operators & Preliminary Logic
#region If & Conditional
    // "initialValue" & "divisor" are assigned in the "Operators & Preliminary Logic" region
    void IfAndBooleanOperators()
    {
        if ((divisor != 0) && (initialValue / divisor) is var result)
        /* Since && short-circuits with first false, we can safely write "initialValue / divisor",
        as the second expression is only ever evaluated when divisor isn't 0 */
        {
            Debug.Log("Quotient: " + result);
            return; // This empty return means its parent, in this case the method, immediately ends
        }
        // Nothing will happen if divisor = 0

        double unsafeQuotient = initialValue / divisor;
        if (Double.IsInfinity(unsafeQuotient) || Double.IsNaN(unsafeQuotient))
        /* Assuming calculating unsafeQuotient doesn't throw an error,
        we can treat the case where it's ±infinity or 0/0 */
        {
            Debug.Log("Quotient is invalid.");
        }
        /* Nothing will happen if divisor isn't 0 or either initialValue or divisor are already ±infinity
        or Not a Number */
    }

    void IfElseSignDetermination()
    {
        int x = 0;
        if (x > 0)
            Debug.Log("Positive"); // For single-line if/else statements, curly brackets aren't necessary
        else
            Debug.Log("Non-positive");
    }
    // When run, prints "Non-positive", since 0 is not positive

    void ConditionalSignDetermination() // This does the same as above
    {
        int x = 0;
        string sign = (x > 0) ? "Positive" : "Non-positive"; // Parentheses are just for clarity
        // Is x > 0?, then assign sign as "Positive", else assign sign as "Non-positive"
        Debug.Log(sign);
    }

    void IfElseParityDetermination()
    {
        int x = 0;
        if (x % 2 == 0)
            Debug.Log("x is even!");
        else if (x % 2 == 1)
            Debug.Log("x is odd!");
        else
            Debug.Log("x is not an integer.");
    }
    /* When run, prints "x is even!", since 0 is even
    The final else is in case the provided value is not an integer, which can't happen since x is an int,
    but y'know, it's an example of if-else-if-else */

    void ConditionalParityDetermination() // This does the same as above
    {
        int x = 0;
        string parity = (x % 2 == 0) ? "x is even!" :
                        (x % 2 == 1) ? "x is odd!" : "x is not an integer."; // Indentation just for clarity
        /* condition1 ? result1 : condition2 ? result2 : result3 is parsed as
        condition1 ? result1 : (condition2 ? result2 : result3)
        We use parentheses for clarity */
        Debug.Log(parity);
    }

    string HighlyNestedConditionalGradeLetter(double score)
    {
        return  score >= 90 ? "A" : // This doesn't have any parentheses & is still easy to read
                score >= 80 ? "B" :
                score >= 70 ? "C" :
                score >= 60 ? "D" : "F";
    }
    // This takes a double as an input & outputs a letter grade string

    bool ValidatePasswordIfNesting(string pass, string passConfirm) // Nesting too deep is bad code design
    {
        if ( ! (pass.Equals(string.Empty) || passConfirm.Equals(string.Empty))) /* Is neither password empty?
        (!) made more obvious by adding spaces before & after; this isn't necessary */
        {
            if (pass.Length >= 8 && passConfirm.Length >= 8) // Are passwords 8 or more characters long?
            {
                if (pass.Equals(passConfirm)) // Do passwords match?
                {
                    return true; // This return applies to its parent "if" statement
                }
                else
                {
                    Debug.Log("Passwords do not match");
                    return false; // The return value of this if/else cascades to outer "if" statements
                }
            }
            else
            {
                Debug.Log("Password length must be 8 or more characters");
                return false; // This also returns to its parent "if" statement
            }
        }
        else
        {
            Debug.Log("Passwords cannot be empty");
            return false; // This returns to method, so unless previous 3 ifs were true, the method returns false
        }
        
    }
    // To case guard correctly & avoid over-nesting, extract each condition outside-in & invert the conditions
    bool ValidatePasswordCodeGuarding(string pass, string passConfirm)
    {
        if (pass.Equals(string.Empty) || passConfirm.Equals(string.Empty)) /* Is either password empty?
        Note that all that was need to invert was to remove the "not" (!) before the OR statement */
        {
            Debug.Log("Passwords cannot be empty");
            return false;
        }
        if (pass.Length < 8 || passConfirm.Length < 8) /* Is either password shorter than 8 characters?
        This condition is more complex. We must invert the greater-than-or-equal-to to less-than for both
        But we must also change && to ||, since instead of needing both to be >= 8, if either are less than 8,
        we must alert the user
        Logically, the inverse of AND is: the [inverse of the inputs] OR'd together (De Morgan's law) */
        {
            Debug.Log("Password length must be 8 or more characters");
            return false;
        }
        if (!pass.Equals(passConfirm))
        {
            Debug.Log("Passwords do not match");
            return false;
        }
        return true; /* Only if none of the conditions above are caught will it output true
        This also makes it so much easier to add new conditions */
    }

// Brief Random Number Generation Tangent Part 1
    void IfLuckyNumber()
    {
        int randomLuckyNumber = UnityEngine.Random.Range(1, 10); /* Picks a random integer between 1-10
        Both System & UnityEngine have a Random method, so even though only the latter has Random.Range,
        I must appease VSC's warning detection by making this explicitly UnityEngine's class method
        Also, this must be called within Start() or Awake(), which is why I put it into the method */
        string randomResult = (randomLuckyNumber == 7) ? "The lucky number was, in fact, 7!" :
        "The lucky number was not 7.";
        Debug.Log(randomResult);
    }

    int ConditionalRandomDamageAmount(bool isHardDifficulty)
    {
        int maxEnemyDamage = isHardDifficulty ? 200 : 100; /* If hard, choose 200 as highest damage amount
        Note that we can include conditional statements in variable assignment */
        return UnityEngine.Random.Range(50, maxEnemyDamage); // Use max to determine damage to deal
    }
#endregion If & Conditional
#region Switch
    int day = 4;
    void SwitchDayOfWeek()
    {
        switch (day)
        {
        case 1: // Cases don't normally need curly brackets
            Debug.Log("Today is Sunday.");
            break; // break is needed to end a case
        case > 1 and <= 4:
        /* We can use inequalities to join cases. "or" & parentheses may also be used for advanced logic
        This will act on 2, 3, & 4 */
            Debug.Log("It's like the week just started.");
            break;
        case < 6: /* Since the earlier cases cover ints 1-4, we can simply use < 6 to cover cases 5 & 6
        Beware, as this technically also applies to integers 0 & below,
        so be rigorous with variable assignment & cases */
            Debug.Log("The week is nearing its end.");
            break;
        case 7:
            Debug.Log("Today is Saturday, my favorite day of the week!"); break;
            // We can also add break to case statement line
        default: /* We don't expect any other values (< 6 technically covers negative, though)
        But it's best practice to always have a default case,
        unless we have all possible values accounted for, which is possible for integers */
            Debug.Log("This is an invalid day."); break;
        }
    }
    // Prints "It's like the week just started."

    void SwitchCompactDayOfWeek() // This does the same thing as above
    {
        var stringOfDay = day switch
        {
            1 => "Today is Sunday.", // Note that commas separate cases in this syntax
            > 1 and <= 4 => "It's like the week just started.",
            < 6 => "The week is nearing its end.",
            7 => "Today is Saturday, my favorite day of the week!",
            _ => "This is an invalid day." // Default case is represented with an underscore
        }; // Note the semicolon, as this is still a variable assignment, despite the appearance of a method
        Debug.Log(stringOfDay);
    }

    string SwitchGradeLetter(double score) /* Since this returns a value, it's not void
    More on access modifiers later */
    {
        return score switch
        {
            >= 90 => "A",
            >= 80 => "B",
            >= 70 => "C",
            >= 60 => "D",
            _ => "F"
        };
    }
    //This is identical in function to HighlyNestedConditionalGradeLetter()

    string SwitchCaseGuardNumberSize(int value)
    {
        switch (value)
        {
            case < 0: return "This is negative."; // Since we use "return", "break" would be unreachable code
            case < 10 when value % 2 == 0: return "This is a small, even number.";
            // "when" acts like and (&&), the code after "when" & before the colon is known as a case guard
            case < 10: return "This is a small, odd number.";
            case >= 10: return "This is a large number.";
            //default: return "unknown";
            // Since all possible int values are accounted for, the above is unreachable code
        }
    }

    string SwitchMethodCompactNumberSize(int value) => value switch // Does same as above
    {
        < 0 => "This is negative.",
        < 10 => "This is a small number.",
        >= 10 => "This is a large number."
    };

// Brief Random Number Generation Tangent Part 2
    enum Color { White, Orange, Magenta, Yellow, LightBlue, Lime, Pink, Gray,
    LightGray, Cyan, Purple, Blue, Brown, Green, Red, Black }
    // enum creates a class that represents unchanging constants; more on these later
    Color GetRandomColor()
    {
        Array colorValues = Enum.GetValues(typeof(Color)); // Gets all enum values
        if (colorValues.Length == 0) // Handles case of empty enum
            throw new InvalidOperationException("Color enum has no defined values.");
        System.Random randomNumber = new(); /* This syntax is shorthand for
        "System.Random randomNumber = new System.Random();" */
        // Both Unity & System have a random method, so I chose System.Random
        int randomColorValue = randomNumber.Next(colorValues.Length); // Generates random enum index
        return (Color)colorValues.GetValue(randomColorValue);
    }
    
    void SwitchMultiCaseColors()
    {
        Color randomColor = GetRandomColor();
        string randomColorOpinion; // No need to type out: string randomColorOpinion = "";
        switch (randomColor) // VSC really wants to convert this to form below (SwitchMultiCaseColorsCompact)
        {
            case Color.White: // We can "hang" cases that will be handled by one below
            case Color.Gray: // This means we don't need to repeat the same code for multiple cases
            case Color.LightGray:
            case Color.Black:
                randomColorOpinion = "Well that's boring."; break;
            case Color.Orange:
            case Color.Yellow:
            case Color.Red:
                randomColorOpinion = "Ooh, one of those autumn colors."; break;
            case Color.LightBlue:
            case Color.Blue:
                randomColorOpinion = "That's the best color ever!"; break;
            default:
                randomColorOpinion = "Not my favorite..."; break;
        };
        Debug.Log(randomColorOpinion);
    }

    void SwitchMultiCaseColorsCompact() // More compact form of above
    {
        Color randomColor = GetRandomColor();
        string randomColorOpinion = randomColor switch
        /* Note that in this form, we can both initialize randomColorOpinion as a string
        & introduce the switch statement on a single line */
        {
            Color.White or Color.Gray or Color.LightGray or Color.Black => "Well that's boring.",
            Color.Orange or Color.Yellow or Color.Red => "Ooh, one of those autumn colors.",
            Color.LightBlue or Color.Blue => "That's the best color ever!",
            _ => "Not my favorite..."
        };
        Debug.Log(randomColorOpinion);
    }

    void SayColorFact(Color color, string colorStatement) // This method takes 2 parameters, more on this later
    {
        Debug.Log("This color, " + color.ToString() + ", " + colorStatement);
    }

    void SwitchGoToDyeColors(Color color)
    {
        switch (color)
        {
            case Color.White:
            case Color.Black:
            case Color.Brown:
            case Color.Red:
            case Color.Yellow:
            case Color.Green:
            case Color.Blue:
                SayColorFact(color, "is a primary dye."); break;
            case Color.LightGray:
                SayColorFact(color, "can be made from gray & white dye.");
                goto case Color.Gray; /* This case, Color.LightGray, will execute the above code
                as well as everything downstream of Color.Gray (which is just the case below) */
            case Color.Gray:
                SayColorFact(color, "can be made from black & white dye."); break; // Executed by LightGray as well
            case Color.Orange:
                SayColorFact(color, "can be made from red & yellow dye."); break;
            case Color.Lime:
                SayColorFact(color, "can be made from white & green dye."); break;
            case Color.Cyan:
                SayColorFact(color, "can be made from blue & green dye."); break;
            case Color.LightBlue:
                SayColorFact(color, "can be made from white & blue dye."); break;
            case Color.Purple:
                SayColorFact(color, "must be made from red & blue dye."); break;
            case Color.Magenta:
                SayColorFact(color, "can be made from red & white dye.");
                // Can't goto both purple & pink, so pasting the pink logic above
                SayColorFact(color, "can be made from pink & purple dye.");
                goto case Color.Purple; // The destination of goto doesn't need to be below. In this case, it's just above this case
            case Color.Pink:
                SayColorFact(color, "can be made from red & white dye."); break;
            default: // "goto default" is also an option, but unnecessary in this method
                SayColorFact(color, "is not a dye in Minecraft."); break;
        }
    }
    // E.g. for LightBlue, this prints: This color, LightBlue, can be made from white & blue dye.

    string SwitchTwoVariablesRockPaperScissors(string first, string second)
    {
        return (first, second) switch // We can use 2 or more variables, but there are more combinations to handle
        {
            ("rock", "paper")       or  ("paper", "rock")       => "Paper wins!",
            ("rock", "scissors")    or  ("scissors", "rock")    => "Rock wins!",
            ("paper", "scissors")   or  ("scissors", "paper")   => "Scissors wins!",
            _ => "Tie."
        };
    }

    void SwitchTuple()
    {
        var tupleCoords = (1, 2);
        string result = tupleCoords switch
        {
            (0, 0)      => "Point lies on origin",
            ( > 0, > 0) => "Point lies in quadrant I", // NE
            ( < 0, > 0) => "Point lies in quadrant II", // NW
            ( < 0, < 0) => "Point lies in quadrant III", // SW
            ( > 0, < 0) => "Point lies in quadrant IV", // SE
            (0, _)      => "Point lies on X axis",
            (_, 0)      => "Point lies on Y axis",
            //_           => "No match" // Unreachable code; all possibilities handled
        };
        Debug.Log(result); // Prints: Point lies in quadrant I
    }
#endregion Switch
#region Loops
// for
    void ForCountToSeven()
    {
        for (int i = 0; i < 7; i++) // Increment i each time this runs until it reaches 7
        /* Literally, this says: Starting with i beng 0, until i is 7,
        run the following & then increment i by 1
        Similarly, we can use i-- to decrement by 1 each time */
        {
            Debug.Log(i + 1);
        }
    }
    // When run, prints "1", "2", "3", "4", "5", "6", & "7"

    void ForContinueOddNumbers()
    {
        int answer = 0;
        for (int i = 0; i < 1; i++)
        {
            if (i % 2 == 0) // If even
            {
                continue; /* Skips current iteration of loop,
                meaning none of the even numbers reach the "answer += i" below */
            }
            answer += i;
        }
        Debug.Log("The answer is {answer}"); // Adds only odd numbers 1-9, so: The answer is 25
    }

    float averageScore = 0f;
    float ForBreakQuizScores(int[] scores)
    {
        averageScore = 0; // Setting back to 0 in case this runs multiple times
        for (int i = 0; i < scores.Length; i++)
        {
            if (scores[i] != -1)
            {
                averageScore += scores[i];
            }
            else
            {
                averageScore = 0;
                break; // Exists for loop entirely, canceling further iterations
            }
        }
        if (averageScore != 0f || scores.Length != 0) // If neither averageScore nor the array length are 0
        {
            averageScore /= scores.Length;
            return averageScore;
        }
        else
        {
            return averageScore; // If averageScore is never set in the if above, then it's 0
        }
    }
    int[] quiz1Scores = {76, 68, -1, 95, 88, 97};
    int[] quiz2Scores = {67, 86, 100, 59, 88, 79};
    void TryDifferentQuizScores()
    {
        Debug.Log(ForBreakQuizScores(quiz1Scores)); // Prints: 0, since one of the scores is -1
        averageScore = 0;
        Debug.Log(ForBreakQuizScores(quiz2Scores)); // Prints: 79.83334
    }

// foreach
    string[] daysOfWeek = {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"};
    void ForEachListDaysOfWeek()
    {
        string message = "";
        foreach (string day in daysOfWeek) // Run the loop for each element in the array (7 times in this case)
        {
            message += $"{day}\n";
        }
        Debug.Log(message);
    }
    /* When run, prints:
    Sunday
    Monday
    Tuesday
    Wednesday
    Thursday
    Friday
    Saturday
    */

    float ForEachBreakQuizScores(int[] scores) // Functionally equivalent to ForBreakQuizScores()
    {
        averageScore = 0;
        foreach (int score in scores)
        {
            if (score != -1)
            {
                averageScore += score;
            }
            else
            {
                averageScore = 0;
                break;
            }
        }
        if (averageScore != 0 || scores.Length != 0)
        {
            averageScore /= scores.Length;
            return averageScore;
        }
        else
        {
            return averageScore;
        }
    }

// while
    int whileHealth = 1738;
    void WhileIncrementHP()
    {
        int i = 0;
        
        while (whileHealth < 1984 && i < 1000) // This makes the while loop happen at most 1000 times
        {
            ++whileHealth; // Increment health by 1 each time this runs & use new value below
            Debug.Log("HP increasing! Health: " + whileHealth);

            i++;
        }
    }
    /* When run, only if the condition "i < 1984" is true will it print "HP increasing! Health: " & the health
    & it will continue to run until the condition stops being true
    Make sure to control the bounds, or this could run for a long time! */

// do
    int doWhileHealth = 100;
    void DoWhileDamagePlayer()
    {
        do
        {
            if (doWhileHealth < 1)
                break;
            // This if statement & break will cancel the loop if the condition is met
            --doWhileHealth; // Decrement health by 1 each time this runs
        } while (doWhileHealth > 75);
        Debug.Log($"Taking damage! Health: {doWhileHealth}");
    }
    /* do executes regardless of if the while statement is true & only repeats if it continues to be true
    That means, when run, this will at first take health from 100 to 75 & print "Taking damage! Health: 75"
    Then subsequent runs will take 1 health away, all the way down to 0
    Once doWhileHealth is below 1, the do loop breaks before executing the subtraction/print */
#endregion Loops
#endregion Loops & Logic
#region Enum
    enum CardinalDirection // Type is capitalized
    /* Enumeration types are assigned as a set of constants
    These members are represented with an integral numeric type, such as int or byte, but not float, etc.
    By default, enum values are an int, meaning it can have positive & negative values */
    {
        North, // Enum members are also capitalized. By default, the first numeric value is 0
        East, // 1
        South, // 2
        West // 3
    }
    CardinalDirection myDir = CardinalDirection.West;
    void SayMyVar() { Debug.Log(myDir); } // Prints: West

    enum Month : byte // Defines Month's underlying type as byte instead of int
    {
        January = 1, // By setting the first value to 1, the next value is 1 above this one
        February, // 2
        June = 6, // Values needn't be consecutive; the next value is 1 above this one
        July, // 7
        August, // 8
        September, // 9
        October // 10
    }
    int myMonthNumber = (int)Month.September;
    void SayMonthNumber() => Debug.Log(myMonthNumber); // Prints: 9

// enum Logic
    enum Shape { Triangle, Square, Star, Hexagon, Circle, Rectangle, Rhombus, Parallelogram };
    enum Device { PC, Mac, iPhone, Android }

    Shape myFaveShape = Shape.Hexagon;
    Device myDevice = Device.PC;
    Shape yourFaveShape = Shape.Parallelogram;
    Device yourDevice = Device.iPhone;
    Device ourDevices = Device.PC | Device.iPhone;
    Device theirDevices = Device.Mac | Device.iPhone;

    void EnumShapesAndDevices()
    {
        Debug.Log("My favorite shape is the " + myFaveShape);
        Debug.Log("Your favorite shape is the " + yourFaveShape);
        Debug.Log("Do we use the same device? " + (myDevice.Equals(yourDevice) ? "Yes." : "No."));
        /* The parentheses here are all necessary. Equals() returns true if 2 values of the same type are the same,
        meaning myFaveShape.Equals(Device.Android) would return false,
        even though the values of both "Hexagon" & "Android" are 3
        Prints: Do we use the same device? No
        */

        Debug.Log(ourDevices); // Prints: PC, iPhone
        Debug.Log("The devices our groups share in common is/are the " + (ourDevices & theirDevices));
        /* Bitwise AND
        Prints: The devices our groups share in common is/are the iPhone
        */
    }

    enum Instrument { None = 0, Piano = 1, Keyboard = 1, Violin = 2, Viola = 2 };
    // Repeated values with different keys

    void EnumInstruments()
    {
        Instrument musician1 = Instrument.Keyboard;
        Instrument musician2 = Instrument.Piano;

        if (musician1.Equals(musician2))
            Debug.Log("For all intents & purposes, they play the same instrument.");
        // Since Instrument.Keyboard & Instrument.Piano have the same type (Instrument) & value, they return equal
    }

    enum Season { Spring, Summer, Autumn, Winter }
    string SeasonsGreeting(Season season) => season switch
    {
        Season.Spring => "Birds are singing, flowers are blooming.",
        Season.Summer => "Long days, warm nights, and endless roads.",
        Season.Autumn => "The leaves fall, and the wind carries their stories across the land.",
        Season.Winter => "Sneezon's greeblings!",
        _ => throw new ArgumentOutOfRangeException(nameof(season))
        /* If an undefined value is called, such as (Season)47, the above exception will handle it
        This is done in case there are any constants added to the enum
        VSC underlines "switch" if there's a missing value when you comment-out the exception */
    };

// Flagged enums
    [Flags] // Flagging an enum allows for multiple values to be utilized. This is an example of a C# attribute
    enum ThreeBitColors // Flagged enums should be named as a plural noun, while unflagged enums are singular
    {
        Black = 0, // Usually, this is "None", but I'm calling this "Black" as it's the lack of any color
        Red = 1, // After 0, assign the next constant's value as 2^0 (1)
        Green = 2, // Then 2^1 (2)
        Blue = 4 // Then 2^2 (4), etc.
        /* Each value should be a power of 2
        For n explicitly assigned members (except the 0 case), the total member count is 2^n,
        So the final member has value 2^n - 1
        In other words, take the next highest power of 2 after the highest assigned value;
        the final member will be one less than that (in this case, 8 - 1 = 7) */
    };
    void ListThreeBitColors()
    {
        for (int val = 0; val < 8; val++)
            Debug.Log(val + " - " + (ThreeBitColors)val);
    }
    /* Prints:
    0 - Black
    1 - Red
    2 - Green
    3 - Red, Green
    4 - Blue
    5 - Red, Blue
    6 - Green, Blue
    7 - Red, Green, Blue
    */

    [Flags]
    enum Sports // This is especially helpful for longer lists, which shouldn't be created by hand
    {
        None = 0,
        Basketball = 1,
        Soccer = 2,
        Football = 4,
        Tennis = 8,
        Lacrosse = 16,
        Baseball = 32,
        Hockey = 64,
        Volleyball = 2^7, // 128
        ESports = 2^8, // 256
        Other = 2^9, // 512
        AmericanGames = Basketball | Football | Baseball // Having multiple flags is done with the bitwise OR operation
    }

    void FlaggedEnumBitwiseLogicSports()
    {
        var Luke = Sports.Basketball | Sports.Football | Sports.Volleyball | Sports.Tennis; // Bitwise OR
        var Brandon = Sports.AmericanGames | Sports.Lacrosse | Sports.Hockey;
        var Sam = Sports.Basketball | Sports.Soccer | Sports.Hockey | Sports.ESports | Sports.Other;
        var likesUSSports = Sports.Basketball | Sports.Football;
        Debug.Log($"It is {Brandon.HasFlag(likesUSSports)} that Brandon likes the 3 big US sports");
        /* HasFlag() determines whether the player likes the input sports,
        in this case, basketball, football, & baseball
        This could've been explicitly stated as Brandon.HasFlag(Basketball | Football | Baseball)
        Prints: It is true that Brandon likes the 3 big US sports
        */
        bool lukeLikesUSSports = (Luke & likesUSSports) == likesUSSports; // bitwise AND
        bool samLikesUSSports = (Sam & likesUSSports) == likesUSSports;
        if (lukeLikesUSSports && samLikesUSSports) // Do Luke AND Sam (both of them) like US sports?
        {
            Debug.Log("Luke & Sam both like one or multiple US sports.");
        }
        // The above is the only one to execute, since both of them like basketball
        if (lukeLikesUSSports ^ samLikesUSSports) // Do Luke XOR Sam (only one of them) like US sports?
        {
            Debug.Log("Either Luke or Sam like one or multiple US sports.");
        }
        if (!(lukeLikesUSSports && samLikesUSSports)) // Do Luke NAND Sam (neither of them) like US sports?
        {
            Debug.Log("Neither Luke nor Sam like US sports.");
        }
        // We cannot use bitwise "&" with None (0-valued enum) because it will always output zero
    }

    [Flags] enum Pets
    {
        None = 0,
        Dog, // 1
        Cat, // 2
        Bird = 4,
        Snake = 8,
        Turtle = 16,
        Hamster = 32
    }

    void FlaggedEnumPets()
    {
        Pets[] petFamilies =
        {
            Pets.None,
            Pets.Dog,
            Pets.Cat,
            Pets.Dog | Pets.Cat,
            Pets.Dog | Pets.Cat | Pets.Bird,
            Pets.Snake | Pets.Turtle
        };

        int familiesWithDogs = 0;
        int familiesWithCats = 0;
        int familiesWithExoticPets = 0;

        foreach (var petFamily in petFamilies)
        {
            if (petFamily.HasFlag(Pets.Dog)) // Counts families that have a dog
                familiesWithDogs++;
            if (petFamily.HasFlag(Pets.Cat))
                familiesWithCats++;
            if (petFamily.HasFlag(Pets.Snake | Pets.Turtle | Pets.Bird))
                familiesWithExoticPets++;
        }
        Debug.Log($"{familiesWithDogs} families have dogs."); // Prints: 3 families have dogs.
        Debug.Log($"{familiesWithCats} families have cats."); // Prints: 3 families have cats.
        Debug.Log($"{familiesWithExoticPets} families have exotic pets."); // Prints: 2 families have exotic pets.
    }
#endregion Enum
#region HashSet
    void SetUpHashSet()
    {
        HashSet<string> famousNames = new()
        {
            "Gerald",
            "Bartholomew",
            "Gilligan",
            "Cornelius"
        };
        famousNames.Add("Bernadette"); // Add() works just like in lists & dictionaries
        famousNames.Remove("Gerald");

        HashSet<string> terribleNames = new()
        {
            "Jar Jar",
            "Bartholomew", // Sorry, Bartholomew
            "Lucifer",
            "Nimrod"
        };

        famousNames.IntersectWith(terribleNames);
        // Finds the common elements between the 2 sets & sets the first one (famousNames) to just those

        string text = "";
        foreach (string name in famousNames)
        {
            text += $"{name}\n";
        }
        Debug.Log(text); // Prints: Bartholomew
        // === Add more: https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.hashset-1?view=net-10.0

        // Note that these aren't the only built-in methods that operate on hashSets
    }
#endregion HashSet
#region Math
    // Incl POW
    // Math uses doubles, Mathf uses floats
    // Mathf: LerpAngle, Clamp01, DeltaAngle, CorrelatedColorTemperatureToRGB
    // Math: Math.Round(val, precision)
    // Include operations on lists, such as Min, Max, Average, etc.
    string[] betaKids = { "John", "Rose", "Dave", "Jade" };
    float[] betaBirthdays = { 4.13f, 12.04f, 12.03f, 12.01f }; // Chose float just to represent different data types
    int counter = 0;

    void KidsBirthdays()
    {
        int minNumberOfItemsInLists = Mathf.Min(betaKids.Length, betaBirthdays.Length);
        if (counter == minNumberOfItemsInLists)
            counter = 0;
        var kid = betaKids[counter];
        var birthday = betaBirthdays[counter];
        Debug.Log($"{kid} was born on {birthday}");
        counter += 1;
    }
    /* To avoid an IndexOutOfRangeException, we use the minimum of (kids, birthdays) & only count up to that number
    Each time this is run, it displays that the nth kid was born on their birthday
    When it reaches the end of the shortest list, it loops back around to the start */
#endregion Math
#region Access Modifiers & Other Keywords
    // Access modifiers determine what can access the code
    private int numOfCharsInString = 40; /* private objects can only be used within the same scope (parent object)
    It is the default for most objects, so everything before has been private (except GameBoard & CSharpTutorial)
    Since it cannot be accessed outside its scope, it cannot be modified either, except through accessors
    Simple types are often camelCase, unless they're constants */
    public double SemiTone = Math.Pow(2d, 1/12d); /* public objects can be accessed anywhere
    It is the default for enum & interface types
    They are basically always written in PascalCase */
    internal string addressMe = "This exists"; // Accessible only within an assembly. Rarely useful in Unity

    // protected objects can be accessed within the scope & by anything that inherits it, so private but inheritable
    class SeventyThreeUser
    {
        static protected byte seventyThree = 73; // static just so we don't need an instance; explained later

        class AnotherSeventyThreeUser // Being nested within the scope, this can access seventyThree, like private
        {
            void SayTheNumber() => Debug.Log(seventyThree);
        }
    }
    class ThirdSeventyThreeUser : SeventyThreeUser // Inheriting from the parent class, it can access seventyThree
    {
        void SayTheNumber() => Debug.Log(seventyThree);
    }
    // structs cannot be protected because they cannot be inherited from

    /* Basically unused for our purposes:
    private protected applies to whatever's in the same assembly or derived class in another assembly
    protected internal applies to whatever's in the same assembly & class/derived class

    These are the access modifiers in order of least to most restrictive:
    public
    protected internal (rarely used)
    protected & internal (rarely used)
        protected can be accessed by a derived class in a different assembly
        internal can be accessed by any class in the same assembly
        but not vice-versa
    private protected
    private */
    private void AccessModifierGibberishHandler()
    {
        List<object> gibberish = new()
        { numOfCharsInString };
        if (gibberish is null) Debug.Log("Gibberish is null");
    }
#endregion Access Modifiers & Other Keywords
#region Methods
    /* Optimization: When worrying about optimization, consider this list:
    Do we have a real, measurable performance issue? If so, continue
    Measure the performance
    Make changes that could improve by 80%, like changing data structures. Measure
    Profile the application & look for hotspots (look up Profiling (computer programming)). Measure
    Analyze what the computer is actually doing under the hood & how memory is allocated/read. Measure
    "If your code doesn't work, it doesn't matter how fast it doesn't work" */

    /* The following methods all lack an explicit access modifier & are thus private
    This is so they aren't accidentally used in the project & this tutorial becomes load-bearing */

    void ExampleUnusedMethod() /* This script doesn't call this method, similarly to all the methods before
    & because this is private, it can't be accessed in any other script */
    {
        Debug.Log("This text won't appear when this script is run because the method is never called.");
        /* Debug.Log() is Unity-specific
        If you want to write to console in a compiler, use Console.WriteLine() */
    }

    void GreetPlanet() /* "void" indicates that this function returns no value to whatever calls it
    This method is called in Start() */
    {
        Debug.Log("Hello world.");
    } // Definition: Everything within the curly brackets (the body)

    void IncrementButDoNotChangeOriginal(int number) // Methods can take parameters like ints & do stuff with them
    {
        number++;
        Debug.Log(number);
    }
    int valueToIncrement = 5;
    void FailToIncrement() // However, beware that by default, methods don't change value types
    {
        IncrementButDoNotChangeOriginal(valueToIncrement); // Prints: 6, since the method's copy is incremented
        Debug.Log(valueToIncrement); // Prints: 5, since the original copy is unaltered
    }
    // We'll see how to actually change the input variable in the "ref" section

    /* If you use VSC, hover over the method name & parameter
    If you don't, use your imagination while reading the triple-commented code */

    /// <summary> Checks if number is even (verbose version) </summary>
    /// <param name="number"> Number to determine the parity of </param>
    /// <returns> true or false </returns>
    bool IsEvenVerbose(int number) /* Declaration: Instead of void, this method's output is bool & its input is int
    This will directly return a value to whatever calls it */
    {
        if (number % 2 == 0)
            return true;
        else
            return false;
    }
    void IsSevenEven()
    {
        if (IsEvenVerbose(7))
            Debug.Log("7 is even.");
        else
            Debug.Log("7 is not even.");
    }

    bool IsEven(int num) // Since the expression "num % 2 == 0" is true or false, we can just return that
    {
        return num % 2 == 0;
    }

    bool IsEvenConcise(int n) => n % 2 == 0;
    /* This can even be done with the (=>) operator to fit methods & certain other assignments on 1 line
    Using (=>) this way means this is an expression-bodied method. That's just what it's called */

// ref Keyword
    void IncrementAndChangeOriginal(ref int number) // With "ref", we can use a direct reference to the value
    {
        number++;
        Debug.Log(number);
    }
    void ActuallyIncrement() // With a reference to original value, the above method changes the original value
    {
        IncrementAndChangeOriginal(ref valueToIncrement); // Prints: 6
        Debug.Log(valueToIncrement); // Prints: 6
    }
    // More on value & reference types at the beginning of the "Classes & Structs" region

// out Keyword
    void GetOutValues(out string a, out int b) /* With "out", we can return multiple values indirectly
    "Out" parameters aren't inputs at all: They're strictly outputs defined in the method
    Like ref, this acts on the value's reference, not just a copy of the value */
    {
        a = "My name is Jeff";
        b = 22;
    }
    string outString = "Greg";
    //int outInt = 21; // We don't actually need to initialize this until the method call
    void ShowcaseOutValues()
    {
        GetOutValues(out outString, out int outInt); // Initializing outInt in the method call
        Debug.Log($"{outString} & my favorite number is {outInt}.");
        // Prints: My name is Jeff & my favorite number is 22.
    }

    bool IsDivisibleBy3(int num, out int quotient) /* The utility of "out" is strongest in Boolean method outputs,
    since we can easily check if a statement is true & set a value at the same time */
    {
        quotient = num / 3; // This will divide by 3 regardless of divisibility; it's up to implementation to check
        return num % 3 == 0; // Checks if num cleanly divides in 3 with no remainder
    }
    void CheckDivisibility()
    {
        string fiftyCheck = IsDivisibleBy3(50, out int fiftyQuotient) ? $"50 divided by 3 is {fiftyQuotient}." :
                                                                        "50 isn't divisible by 3.";
        string sixtyCheck = IsDivisibleBy3(60, out int sixtyQuotient) ? $"60 divided by 3 is {sixtyQuotient}." :
                                                                        "60 isn't divisible by 3.";
        /* If IsDivisibleBy3 is true, state the quotient of num / 3. If false, don't use that int at all,
        but note that fiftyQuotient is still defined as 50 / 3, which for int type is truncated to zero: 16 */
        Debug.Log($"{fiftyCheck}\n{sixtyCheck}");
        /* Prints:
        50 isn't divisible by 3.
        60 divided by 3 is 20.
        */
    }

// in Keyword
    int[] longDigitArray = { 3, 1, 4, 1, 5, 9, 2, 6, 5, 3, 5, 8, 9, 7, 9, 3, 2, 3, 8, 4, 6, 2, 6, 4, 3, 3, 8, 3 };
    decimal ConcatenateDigits(in int[] array) /* With "in", the variable is taken as a reference,
    which for large amounts of data, means no unnecessary copies are made
    Using "in", the variable is read-only, which is important for reference types to avoid changing them */
    {
        decimal longNum = 0;
        foreach (var digit in array)
        {
            longNum *= 10; // Multiply by 10 to move the existing digits to the left by 1 digit
            longNum += digit; // Add digit to final place (which was 0 due to previous line)
        }
        return longNum;
    }
    void PiDigitsMessage() => Debug.Log(ConcatenateDigits(longDigitArray)); /* Prints: 3141592653589793238462643383
    Note that this is just to demonstrate a naive use of "in". This method doesn't scale with large numbers */

// Optional Parameters
    void OptionalParameter(string name = "World") // Defining the variable in the parentheses makes it optional
    {
        Debug.Log($"Hello, {name}!");
    }
    void UseOptionalParameter()
    {
        OptionalParameter(); // Prints: Hello, World!
        OptionalParameter("Geromy"); // Prints: Hello, Geromy!
    }

    void MultipleOptionalParameters(string name, int age, string favGame = "Crazy Frog Racer 2", int playTimes = 99)
    // Note that all the parameters are named & 2 of them are predefined
    {
        Debug.Log($"My name is {name}. I'm {age} years old & I've played through {favGame} {playTimes} times.");
    }
    void UseMultipleOptionalParameters() => MultipleOptionalParameters(age: 26, playTimes: 3, name: "Nicole");
        /* With multiple optional params, we can use the param names in the call in whatever order we want
        Prints: My name is Nicole. I'm 26 years old & I've played through Crazy Frog Racer 2 3 times.
        */

// params Keyword
    void PrintNumbers(string name, params int[] numbers)
    {
        StringBuilder fullMessage = new(); // See the tangent below for how to use StringBuilder
        fullMessage.Append($"{name}: ");
        for (int i = 0; i < numbers.Length; i++)
        {
            if (i < numbers.Length - 1) // For each element in the array, except the last one,
                fullMessage.Append($"{numbers[i]}, "); // Append the element followed by a comma & space
            else
                fullMessage.Append(numbers[i]); // For the last element, simply append the element by itself
        }
        Debug.Log(fullMessage);
    }
    void PrintTheseNumbers()
    {
        int[] favNums = { 1, 6, 18, 0, 3, 39, 8, 87, 4, 9, 89, 48, 4820, 45 };
        PrintNumbers("Secretly the digits of the golden ratio", favNums);
    }
    // Prints: Secretly the digits of the golden ratio: 1, 6, 18, 0, 3, 39, 8, 87, 4, 9, 89, 48, 4820, 45

// Brief StringBuilder Tangent
    string RepeatMessageExpensive(string message, int amount)
    {
        string longMessage = ""; // Initial string
        for (int i = 0; i < amount; i++) // Repeat [amount] times
        {
            longMessage += message + "\n"; // Add "message" & new line to existing string. See *1 below
        }
        return longMessage;
    }
    /* *1 As stated later in the "Classes & Structs" region, strings are immutable,
    meaning the appearance of modifying a string is actually an illusion
    In actuality, a brand new string is created every single time this "for" loop runs
    For that reason, use the below for repeated string concatenation, unless you need the intermediate strings */

    string RepeatMessageCheap(string message, int amount)
    {
        StringBuilder longMessage = new(); // String concatenation is being handled by a dedicated class
        for (int i = 0; i < amount; i++)
        {
            longMessage.Append(message + "\n"); // Adds the message followed by a new line
        }
        return longMessage.ToString(); // Final immutable string
    }
    // This avoids creating unnecessary strings in memory
    void PrintMessage() => Debug.Log( RepeatMessageCheap("I'm a goofy goober, yeah", 6) ); // Prints 6 times
    /* Because the message is so long vertically, if you put this in start() or something,
    you'll have to drag the bottom portion up to see the other lines */

    string StringBuilderStuff(string message, int amount)
    {
        StringBuilder longMessage = new(message);
        longMessage.Append('\t', amount); // With Append(), we can add repeated characters [amount] times
        longMessage.AppendLine(); // Appends a new line
        longMessage.Replace("\t\t", "~"); // Replaces every instance of first param with second param
        longMessage.Remove(0, amount); // Removes everything from array index of first param to second
        longMessage.Insert(0, "Texas Unicycle Parade"); // Inserts second param at index of first param

        StringBuilder anotherMessage = new("Yo, what's up, planet peeps?");
        string thirdMessage = anotherMessage.Remove(0, 14).Replace("planet peeps?", "Hello").Append(" World")
                                            .ToString(); // We can chain together methods like so
        return thirdMessage; /* It's pretty bad to have inputs for a method just to do output anything with them
        But this is a brief tangent, so please forgive me */
    }

// === What is even this?
    static double Diff1(double x, Func<double, double> f) // Func<> returns a value
    {
        double h = 0.0000001;
        return (f(x + h) - f(x)) / h;
    }

    class Program
    {
        static void Main()
        {
            double result = Diff1(2.0, x => Math.Sqrt(x));
            Debug.Log(result);
        }
    }

    static void Process(Action<int> action, int value) // Action<> returns void
    {
        action(value);
    }

    class Program2
    {
        static void Main2()
        {
            Process(x => Debug.Log("Value: " + x), 42);
        }
    }

// Generic Types
    T[] CreateAnyTypeArray3<T>(T element1, T element2, T element3) // This array is generic: It works with any type
    // T is the common stand-in for any type. You can use AnyType if it's clearer to you
    {
        return new T[] { element1, element2, element3 };
    }
    void DoSomethingWithAnyTypeArray()
    {
        float[] arrayOfFloats = CreateAnyTypeArray3(7.2f, 8, 9);
        // At least one of them must be formatted like a float
        Debug.Log($"{arrayOfFloats.Length}: {arrayOfFloats[0]} {arrayOfFloats[1]} {arrayOfFloats[2]}");
        // Prints: 3: 7.2 8 9
        Debug.Log(CreateAnyTypeArray3("A", "B", "C").GetType()); // Prints: System.String[]
    }

    void MultiTypeGenericMethod<T1,T2>(T1 item1, T2 item2)
    {
        Debug.Log($"{item1.GetType()} - {item1}\n{item2.GetType()} - {item2}");
    }
    void DoSomethingWithMultiTypeGenericMethod() => MultiTypeGenericMethod("Generic, huh?", 12.34f);
    // <string, float> can be inferred implicitly
    /* Prints:
    System.String - Generic, huh?
    System.Single - 12.34
    (Floats are internally known as singles) */
#region Write Clearer Code
    bool isAuthorized = true;
    bool isAuthenticated = true;
    bool isMaintenancePeriod = false;
    string whichContinent = "NA";
    Dictionary<string, float> shippingPerContinent = new()
    {
        ["NA"] = 4.99f, // [Value] = key
        ["SA"] = 7.99f,
        ["EU"] = 12.49f,
        ["AS"] = 7.99f, // Values may be duplicated, but not keys; they must be unique
        ["AF"] = 9.99f,
        ["OC"] = 14.99f,
    };
    

    void AddAntarctica() // Since this is never called, it never runs
    {
        shippingPerContinent.Add("AQ", 19.99f); // This is a way to add to a dictionary after initialization
    }

    void UnreadableCodeOne() // Don't code like this
    {
        if (!isMaintenancePeriod)
        {
            if (isAuthorized)
            {
                if (isAuthenticated)
                {
                    Debug.Log("Shipping cost: " +
                    shippingPerContinent.FirstOrDefault(pair => pair.Key == whichContinent).ToString());
                }
                else
                {
                    Debug.Log("Error: Authentication failed.");
                }
            }
            else
            {
                Debug.Log("Error: You are not authorized to view this page.");
            }
        }
        else
        {
            Debug.Log("Error: Site currently under maintenance.");
        }
    }
    /* While this code functions, it's hard to follow at first glance. What we can do is extract
    some functionality as separate methods, like determining the shipping cost (safely) & maybe
    an error message, since seeing duplication indicates there's something we can simplify */

    float FindShippingCost(string continent)
    {
        float shippingPrice = 14.99f; // Assigning shippingPrice early in case try() fails
        try
        {
            shippingPerContinent.TryGetValue(continent, out shippingPrice);
        }
        catch (KeyNotFoundException) // System exception in case of invalid key
        {
            ErrorMessage($"Continent code \"{continent}\" is invalid.");
        }
        return shippingPrice;
    }

    private void ErrorMessage(string message)
    {
        Debug.Log("Error: " + message);
    }

    void UnreadableCodeTwo() // Don't code like this
    {
        if (!isMaintenancePeriod)
        {
            if (isAuthorized)
            {
                if (isAuthenticated)
                {
                    Debug.Log("Shipping cost: " + FindShippingCost(whichContinent).ToString());
                }
                else
                {
                    ErrorMessage("Authentication failed.");
                }
            }
            else
            {
                ErrorMessage("You are not authorized to view this page.");
            }
        }
        else
        {
            ErrorMessage("Site currently under maintenance.");
        }
    }
    /* It's still highly nested & somewhat hard to follow
    The next step to fixing this method is to utilize inversion & return to avoid nesting */

    void ReadableCodeOne() // This is easier to read
    {
        if (isMaintenancePeriod)
        {
            ErrorMessage("Site currently under maintenance.");
            return; // Exits method early, all else is ignored
        }
        if (!isAuthorized)
        {
            ErrorMessage("You are not authorized to view this page.");
            return;
        }
        if (!isAuthenticated)
        {
            ErrorMessage("Authentication failed.");
            return;
        }
        var shippingCostAsString = FindShippingCost(whichContinent).ToString(); // Runs only if none return
        Debug.Log($"Shipping cost: {shippingCostAsString}");
        // To increase legibility, I split up the shipping cost code into 2 lines
    }
#endregion Write Clearer Code
// Delegates
    // === We can act on parameters by passing it as a delegate (like Func<>, Action<>, or a custom one)
    delegate double MyFunction(double x); // This is a custom delegate, made to make intent explicit
    static double Diff2(double x, MyFunction f)
    {
        double h = 0.0000001;
        return (f(x + h) - f(x)) / h;
    }

    double result = Diff2(2.0, x => x * 4); // Lambda expression

    // === NOT RECOMMENDED TO DEFINE DELEGATE TYPES, CONSIDER DROPPING ABOVE

    // Use of Action
    /* public class Example : MonoBehaviour
    {
        public Action myAction; // Zero‑argument action

        void Start()
        {
            myAction += () => Debug.Log("Action called!");
            myAction += () => Debug.Log("Second action called!");
        }

        void OnButtonClick()
        {
            myAction?.Invoke(); // Calls all registered actions
        }
    } */


// LINQ (Language-Integrated Query)
    /*
    int[] numbers = { 5, 10, 8, 3, 6, 12 };
    var evenNumbersA = from n in numbers // Query syntax
        where n % 2 == 0
        select n;

    var evenNumbersB = numbers.Where(n => n % 2 == 0); // Method syntax

    var containsPizza = daysOfWeek.Where(s => s.Contains("sday"));

    var doubleNumbers = numbers.Select(n => n * 2);

    var orderNumbers = numbers.OrderBy(n => n);

    var groupNumbers = numbers.GroupBy(n => n % 2);

    var aggregateNumbers = numbers.Aggregate((a, b) => a + b);

    int countNumbers = numbers.Count();
    int sumNumbers = numbers.Sum();
    int maxOfNumbers = numbers.Max();
    int minOfNumbers = numbers.Min();
    int averageOfNumbers = numbers.Average();
    */
    private void TestPerformance() // This method determines how long it takes to execute some simple math
    {
        decimal explodingNum = 1e-28m; // Smallest possible decimal number
        Stopwatch stopwatch = Stopwatch.StartNew();

        stopwatch.Start();
        while (explodingNum < 7.846e28m) // Multiplies by two 189 times, before reaching our maximum
        {
            explodingNum *= 2;
        }
        stopwatch.Stop();

        long elapsedTimeMilliseconds = stopwatch.ElapsedMilliseconds; // Precision of only 1 millisecond
        long elapsedTicks = stopwatch.ElapsedTicks; // Precision of ~100 nanoseconds
        long nanoseconds = elapsedTicks * 1_000_000_000L / Stopwatch.Frequency; /* 1s = 1B ns
        Frequency may not be exactly 10,000,000 ticks per second
        Stopwatch.Frequency is static, so we call the class, not the instance */
        Debug.Log($"{elapsedTimeMilliseconds} ms\n{nanoseconds} ns");
    }
#endregion Methods
#region Classes & Structs
// Value & Reference Types
    /* Value types are types that actually contain their data & exist temporarily in the stack
    The stack is a region in memory that stores value types, method parameters, local variables, & return addresses
    The stack uses a LIFO (last in, first out) structure, so when the deepest branch of code finishes executing,
    the relevant data is purged from memory. It's meant for quick access, short-lived data used by methods
    Essentially, value types are used by creating real copies of the type in memory
    Note that value types can't normally be null, hence why they must be initialized specifically to be nullable:
    int? n = 0;

    Value types:
        Boolean
        Numeric types, including integrals, float, double, & decimal
        Tuple
        Struct
        Enum

    Value type behavior:
        int x = 2;
        int y = x;
        x = 4;
        y = 5;
        Debug.Log(x); // Prints: 4
        Debug.Log(y); // Prints: 5
    In the above example, y is assigned the VALUE of x; it is an independent copy of the original data
    Thus, when we reassign both variables, we see they can be manipulated independently

    Reference types, on the other hand, are data directly referenced by the code,
    meaning changes made in 1 method will affect the object itself, not a copy
    Reference types exist in the heap for as long as references point to them
    An object's reference count begins at 1 at initialization & increases as other code references it
    Once its reference count reaches 0, it will be cleared by the garbage collector
    The garbage collector takes processing power to search through the heap for unreferenced objects
    Thus, for performance, it is better to reuse objects than to delete & recreate them
    Reference count decreases by the object leaving some scope (such as an if block returning),
    or by the object being set to null
    Thus, a null class may be deleted by the garbage collector & trying to reference it can crash the program!
    Also note that reference types are natively nullable

    Reference types:
        String  See *2 below
        Array
        Object
        Class
        Delegate
        Interface
        Dynamic
    Method parameters are Value types when passed by a Struct & Reference types when passed by a Class

    Reference type behavior:
        class Car
        {
            int Doors;
            Car(int doors) { Doors = doors; }
        }
        private void MakeCars()
        {
            Car myCar = new Car(2);
            Car yourCar = myCar;
            myCar.Doors = 4;
            yourCar.Doors = 5;
            Debug.Log(myCar.Doors); // Prints: 5
            Debug.Log(yourCar.Doors); // Prints: 5
        }
        MakeCars();
    In this case, yourCar is assigned as a REFERENCE to myCar; both myCar & yourCar point to the same object
    Thus, when we reassign both variables, we're really reassigning the same thing through different references,
    showing that initializing an object with a reference object means they are connected

    If we replace "class" in the above with "struct", then the compiler treats myCar & yourCar as value types, meaning myCar.Doors is 4

    *2  Despite being reference types, strings do NOT exhibit the above behavior
    This is because they are immutable; they cannot be altered. Instead, a whole new object is created
    Take the following example:
        string result = "";
        for (int i = 0; i < 1000; i++)
        {
            result += i.ToString(); // Adds the number i as a string to "result"
        }
    This code creates 1000 new strings in memory, in addition to the initial one, so is highly inefficient
    Instead, use StringBuilder(), as shown in  */
#region Classes
// === CAPITALIZE ANON TYPES
    /* When naming classes, use PascalCase
    Name the parent something basic & the child something specific, such as Truck > TrailerTruck
    Reorganize code blocks to avoid naming things "Utils" or "HelperFunctions" into properly named classes
    So if you're having trouble naming something, something may need to be restructured */

    public class DogOwner // Use PascalCase for class names & properties
    {
        public string Name; // This is a property, accessible in instances
        public int OwnerAge;
        public string PhoneNumber;

        public DogOwner() {} // This empty constructor allows us to declare an empty instance

        public DogOwner(string ownerName, int ownerAge, string phoneNumber)
        // This constructor lets us immediately populate DogOwner's properties as method parameters
        {
            Name = ownerName; // This is a field, which uses camelCase
            OwnerAge = ownerAge;
            PhoneNumber = phoneNumber;
            // Properties & fields, as well as methods & events, are collectively called class members
        }
    }

    public class Dog // Using one class in another's definition is called composition
    {
        public string Name; /* Sharing member names between composition (Dog) & component (DogOwner) isn;t great,
        but this can be mitigated, as seen in GetBark() */
        public int DogAge;
        public DogOwner Owner; // DogOwner here is a component in the Dog composition class

        public Dog() {}
        public Dog(string dogName, int dogAge, DogOwner owner)
        { Name = dogName; DogAge = dogAge; Owner = owner; } // All fields can go on same line

        public string GetBark() =>
            $"My name is {this.Name}, my owner is {Owner.Name}"; // Shorthand for return notation
        // "this." isn't strictly necessary here, but it's to distinguish it from Owner.Name

        public void Bark()
        {
            Debug.Log(GetBark()); // GetBark() returns the string with the values from the Dog & DogOwner classes
        }
    }

    public void InstantiateDogClasses()
    {
        DogOwner claudia = new(); // This is the long way to instantiate a class
        claudia.Name = "Claudia C.";
        claudia.OwnerAge = 50;
        claudia.PhoneNumber = "1-800-222-1222";

        Dog jolly = new() // This is the simple way to instantiate a class
        {
            Name = "Jolly",
            DogAge = 1,
            Owner = claudia
        };

        Debug.Log($"{jolly.Name} is quite the troublemaker & {jolly.Owner.Name} has their hands full!");
    }
    // Because the jolly instance is within a method, we can't access it out here

    Dog freddy = new("Frederick", 9, new DogOwner("Rex C.", 23, "830-476-5664")); /* Even more compact assignment
    This is why we made Dog() & DogOwner constructors that each take 3 parameters
    The DogOwner component in this case doesn't get named & is instead a part of freddy as freddy.Owner
    Since freddy is outside of a method, it can be modified by the following method: */

    void ChangeDogValues(Dog dog, int randomNumJustForFun)
    {
        dog.Name = "Puggy";
        dog.DogAge = 3;
        dog.Owner.Name = "Peggy";
        dog.Owner.PhoneNumber = "000-000-0001";
        randomNumJustForFun = 1337;
    }
    void DogSwap()
    {
        int superImportantNum = 43_112_609;
        ChangeDogValues(freddy, superImportantNum);
    }
    /* Values after DogSwap():
    freddy.Name = Puggy
    freddy.DogAge = 9
    freddy.Owner.Name = Peggy
    freddy.Owner.OwnerAge = 23
    freddy.Owner.PhoneNumber = 000-000-0001
    superImportantNum = 43112609
    Note that ChangeDogValues() had no effect on superImportantNum, since it is not a reference type */

    public Dog DogFactory(string name, int age, DogOwner owner)
    // This will create entirely new Dog objects without all the hassle above
    {
        Dog newDog = new(name, age, owner);
        return newDog;
    }

    public void InstantiateNewDogs()
    {
        DogOwner RogerR = new("Roger Radcliffe", 26, "Wouldn't you like to know?");
        Dog clifford = DogFactory("Clifford", 2, RogerR);
        Dog bluey = DogFactory("Bluey", 7, RogerR);
        Dog courage = DogFactory("Courage", 26, RogerR); // It seems Courage has no canonical age

        string dogMessage = $"We're {clifford.Name}, {clifford.DogAge}; {bluey.Name}, {bluey.DogAge}; & {courage.Name}, {courage.DogAge} & we're the Colorful Dogs Club.";
        Debug.Log(dogMessage);
    }

// get & set Accessors, Constructors, Deconstructors, & Inheritance
    // To avoid random access to a class' members, we can create explicit methods to get & set them
    private class ManualCar // Setting this to private because it's suboptimally formatted
    {
        string Owner;
        int Doors;
        int Wheels;
        string GetOwner()
        {
            return Owner;
        }
        void SetOwner(string owner)
        {
            this.Owner = owner; // "this." to be explicit
        }
        int GetDoors() { return Doors; }
        void SetDoors(int doors) { Doors = doors; } // Even on a single line, this is getting tedious
        int GetWheels() => Wheels; // Expression-bodied notation saves 8 characters
        void SetWheels(int wheels) => Wheels = wheels; // Expression-bodied notation saves 1 character
    }
    // Making the getters/setters manually is a lot of boilerplate code to write

    public class AutomaticCar // Easier way to create get/set methods
    {
        public string Owner { get; set; } // We can make the getters & setters (accessors) just like this
        public int Doors { get; set; } // We can make get/set more restrictive, such as being private/protected
        public int Wheels { get; set; }
    }

    void MakeAutomaticCar()
    {
        AutomaticCar civic = new()
        {
            Doors = 4, /* With setters & no constructor, we can set any number of properties & not include others
            using the curly bracket notation */
            Wheels = 4
        };
        Debug.Log($"This civic has {civic.Doors} doors. That's more than my bike has!");
    }

    public class FancyCar
    {
        public string Owner {get; set;}
        private int _doors; /* It's convention for non-public members to be camelCase & preceded by an underscore
        Using a private field like this is called encapsulation */
        private int _wheels; // Unless they're constants, in which case they use their normal PascalCase
        
        public int Doors // This allows us to constrain what inputs are allowed for the properties
        {
            get { return _doors; }
            set
            {
                if (value > 0 && value <= 12) // The car with the most doors I saw was 10, so I made this 12
                    _doors = value;
                else
                    _doors = 2;
            }
        }
        public int Wheels // Identical in structure to above
        {
            get => _wheels;
            set => _wheels = (value > 0 && value <= 1000) ? value : 4; // SPMTs can have 1000+ wheels
        }

        public FancyCar(string O, int D, int W) =>
            (Owner, Doors, Wheels) = (O, D, W); /* Yet another way to write a constructor, with a tuple
            Note the semicolon */
        public FancyCar() : this("Owner Name", 4, 4) // Default values for no parameters; these can also be defined as named fields
        {} /* Declaring an empty body to avoid error CS0501. Note there is no semicolon
        this() is a reference to the existing 3-parameter constructor. This avoids typing out a new definition
        Defining distinct methods with different parameters like this is called overloading */

        public void Deconstruct(out string O, out int D, out int W) =>
            (O, D, W) = (Owner, Doors, Wheels); /* Deconstructing spits out parameters from within class
            Single variables can be done w/o a tuple */
    }

    public class DeLorean : FancyCar // This is called inheritance: DeLorean is a child of the FancyCar parent class
    {
        // The parent class doesn't need to know anything about its children, but the child inherits all its members
        public string Color {get; set;}
        public Vector3 position = new(0,0,0);
        public float moveSpeed = 88f;

        public DeLorean(string owner, string color) : base(owner, 2, 4) => Color = color;
        // We use base() to input the properties of the parent object, FancyCar, & define color as well

        public void Move(Vector3 direction, float speed)
        {
            position += speed * Time.deltaTime * direction;
        }
        public void Move(Vector3 direction) // Overloaded method: We can overload so long as the parameters are (a) different number/type(s), including order
        {
            Move(direction, moveSpeed); // We can call the most complete version of a method to overload & include its definition in derived versions
        }
    }

    void MakeTheFuture()
    {
        DeLorean timeMachine = new("Emmett Brown", "silver");
        Debug.Log($"The {timeMachine.Color} time machine has {timeMachine.Doors} doors.");
        /* We can access the parent-class properties from the child instance
        Prints: The silver time machine has 2 doors.
        */
    }

    private class Vector3D
    {
        float X {get; set;}
        float Y {get; set;}
        float Z {get; set;}
        Vector3D(float x, float y, float z) => (X, Y, Z) = (x, y, z); // 3-parameter constructor
        Vector3D() : this(0f, 0f, 0f) {} // 0-parameter constructor (with default values)
        Vector3D(float w) : this(w, w, w) {} // 1-parameter constructor (sets same to all)

        public static Vector3D TripleSameCoordinate(float w) => new(w);
        /* This method is public & static, unlike the parent class. It calls the 1-param constructor
        This is useful if we want to make the purpose of a method more obvious
        This is equivalent to the below:
        public static Vector3D TripleCoordinate(float w)
        {
            return new Vector3D(w);
        }
        */
    }
    Vector3D origin = Vector3D.TripleSameCoordinate(0);

    public class HumanPerson
    {
        public string BirthName {get; set;}
        public string FamilyName {get; set;}
        public string FullName => $"{BirthName} {FamilyName}";
        /* We can get new variables by processing existing ones
        This is even shorter notation for defining the get method, completely avoiding writing "get" explicitly */
        public HumanPerson(string birthName, string familyName) =>
            (BirthName, FamilyName) = (birthName, familyName);
    }
    HumanPerson DeeFeeCee = new("Douglas", "Correa");
    public void WhoMadeThis() => Debug.Log($"{DeeFeeCee.FullName} made this script.");

// Static Methods & Classes
    public class ScoreCounter
    {
        public int Score {get; set;} // Non-static variables need an instance to be accessed
        public static int TopScore {get; set;} // Static variables are accessible without the need of an instance

        public void SetOwnScore(int value)
        {
            this.Score = value; // "this" is redundant, but see below
            if (this.Score > TopScore)
                TopScore = this.Score;
        }
        public static void SetTopScore(int value)
        {
            //this.TopScore = value; // This is invalid, as static methods don't permit access to instance variables
            /* This is because static methods can be accessed outside of instances entirely, on the class itself
            There is no instance that "owns" the variable, so "this" can't refer to anything */
            if (value > TopScore)
                TopScore = value;
        }
    }

    private static class StaticStringTools // Static classes cannot be inherited
    {
        public static int ReverseCount {get; set;} // Counts how many times Reverse() is used
        public static string Reverse(string word)
        {
            StringBuilder newWord = new();
            for (int i = 0; i < word.Length; i++) // Loop through the amount of letters in "word"
            {
                int letterIndex = (word.Length - 1) - i; // Parentheses just for clarity
                newWord.Append(word[letterIndex]);
                /* Arrays begin at 0, but .Length returns 1 for an array with only a 0 element, so subtract 1
                (For word "A", word.Length = 1 while word[0] = "A". So we must -1 from .Length to sync the two)
                "word.Length - 1" represents the last character in word, so to go backwards,
                we must start there & sequentially subtract to get to the beginning, hence "- i"
                Example: Say word is "BLUE". Like an array, index 0: B, 1: L, 2: U, & 3: E
                for i = 0, letterIndex = (4 - 1) - 0, or 3, & word[3] is E
                for i = 1, letterIndex = (4 - 1) - 1, or 2, & word[2] is U
                for i = 2, letterIndex = (4 - 1) - 2, or 1, & word[1] is L
                for i = 3, letterIndex = (4 - 1) - 3, or 0, & word[0] is B
                Appending to our StringBuilder newWord, we get EULB, the reverse of BLUE
                i = 4 exits the loop */
            }
            ReverseCount += 1;
            return newWord.ToString();
        }
    }
    /* The reason that the int & method above are static is so we can use them outside instances
    For example, we can access ReverseCount outside of any instance; it's also tracked by the class itself
    Similarly, Reverse() can be called independent of an instance, as seen below */

    void UseStaticMethod()
    {
        StaticStringTools.Reverse("dioretsa"); 
        Debug.Log(StaticStringTools.ReverseCount);
    }
    /* Prints:
    asteroid
    1
    */

// Abstract Classes & Virtual & Override Methods
    public abstract class Fighter // This is an abstract class; we will not make any direct instances of a Fighter
    {
        public string Name {get; set;}
        public int Strength {get; protected set;}
        public int Intelligence {get; protected set;}

        public Fighter(string name, int strength, int intelligence) =>
            (Name, Strength, Intelligence) = (name, strength, intelligence);

        public Fighter(string name) : this(name, 0, 0) {}

        public abstract string WhatWeapon(); /* This abstract method is (& must be) empty in the parent class,
        but must be present & defined in child classes */

        public string StateStats() => $"My name's {Name}. I have {Strength} strength & {Intelligence} intelligence";

        public virtual string BattleCry() => $"{StaticStringTools.Reverse(Name)}!";
        /* BattleCry() uses a static method, but Reverse() itself is an instance method, so it must be used on an object
        Setting to "virtual" to later override */

        public override string ToString() /* ToString() is built into classes & is already marked "virtual"
        Normally, it would output: "CSharpTutorial+Samurai", which is the fully qualified class name */
        {
            return $"{Name} - Str: {Strength}. Int: {Intelligence}.";
        }
    }

    public class Barbarian : Fighter // Being a subclass of the abstract Fighter, we can make instances of Barbarian
    {
        public Barbarian(string name) : base(name, 3, 0) {}
        public override string WhatWeapon() => "Axe";
        // This override is necessary, as Weapon() exists in the parent class
    }
    public class Tactician : Fighter
    {
        public Tactician(string name) : base(name, 0, 3) {}
        public override string WhatWeapon() => "Pistol"; // Necessary override
    }

    Barbarian Tacocat = new("tacocat");
    Tactician Enola = new("enola");
    void StaticShowcase()
    {
        Debug.Log($"{Tacocat.BattleCry()}\n{Enola.BattleCry()}");
        /* Prints:
        tacocat!
        alone!
        */
        Debug.Log(StaticStringTools.ReverseCount); // Prints: 2
    }
    
    void AnnounceFighters()
    {
        Debug.Log($"{Tacocat.StateStats()}\n{Enola.StateStats()}");
        /* Prints:
        My name's tacocat. I have 3 strength & 0 intelligence
        My name's enola. I have 0 strength & 3 intelligence
        */
    }

    public class Samurai : Fighter
    {
        public Samurai(string name) : base(name, 2, 2) {}
        public override string BattleCry() // Overrides method in Fighter of same name
        {
            string battleCryAllCaps = base.BattleCry().ToUpper(); // Still uses the base method in definition
            return $"{battleCryAllCaps}";
        }
        public override string WhatWeapon() => "Sword"; // Necessary override
    }
    Samurai Ronin = new("Ronin");
    void RoninSpeak()
    {
        Debug.Log($"My weapon of choice is the {Ronin.WhatWeapon()}."); // Prints: My weapon of choice is the Sword.
        Debug.Log(Ronin.BattleCry()); // Prints: NINOR!
        Debug.Log(Ronin.ToString()); // Prints: Ronin—Str: 2. Int: 2.
    }

// Nested Classes
    public abstract class Container
    {
        private string _value = "Watermelon";

        public class Nested : Container // You can nest a class, even inside an abstract class
        {
            public override string ToString() => _value; // This allows us to access private members

            public class DoubleNested : Nested
            {
                public string FindWatermelonWisdom()
                    => $"{_value}s represent freedom of expression & freedom from destruction.";
            }
        }
    }
    public class NotNested : Container
    {
        //public override string ToString() => _value; // Not being nested, it cannot access private members
        /* Although this would work if _value were instead a protected "Value", it's preferred to make it private
        & give access via setters */
    }
    void NestedClassMethods()
    {
        Container.Nested specialFruit = new(); // The container & nested class must be named like this
        Debug.Log(specialFruit.ToString()); // Prints: Watermelon

        Container.Nested.DoubleNested specialWisdom = new(); // Deeper nestings still access private fields
        Debug.Log(specialWisdom.FindWatermelonWisdom());
        // Prints: Watermelons represent freedom of expression & freedom from destruction.
    }
    
// Singletons
    public class SingletonExample : MonoBehaviour // MonoBehaviour is Unity-Specific
    /* Singletons, when instantiated, exist as the singular scene instance
    Any other script can access the singleton using a static reference to an instance of its type
    We're using MonoBehaviour to utilize Awake() */
    {
        public static SingletonExample Instance { get; private set; } /* This is the instance being defined
        Singletons are globally accessible, hence "public static"
        The "private set" ensures that it can only be set from inside the class
        Also, despite being an instance, this is not lowercase; it is a public property, so we use PascalCase */

        /*
        private void Awake() // Commenting this out so it isn't actually instantiated
        {
            if (Instance is not null && Instance != this) // when Instance is neither null nor this class
            {
                Destroy(gameObject); // There can only be 1 instance
                return;
            }
            Instance = this; // Singletons instantiate themselves
            DontDestroyOnLoad(gameObject);
        }
        */

        // global reference to local script, to give everything easy access to something essential to the scene
    }
#endregion Classes
#region Structs
// === can't inherit, thus can't use protected, doesn't need new keyword
#endregion Structs
#endregion Classes & Structs
#region Interfaces
    // === They define similar behavior for different objects
    public interface ISavable /* Interfaces start with capital I
    They're used to define similar behavior for different objects */
    {
        public void Save(); // All objects which implement this interface must have a defined Save() method
        public string GetCall(); // They must also have a GetCall() method
        public void Eat() => Debug.Log("Nom nom."); // Since this one's defined, it counts as being implemented
    }
    public abstract class Creature // Creature inherits from the ISavable interface
    {
        public string Name { get; protected set; }
        public int Age { get; set; }
        public enum Size { small, large } // Size can either be small or large
        public Size Sizeness;
        
        public Creature(string name, int age, Size aSize) => (Name, Age, Sizeness) = (name, age, aSize);
        /* Save() will be implemented in each subclass individually
        Parameter naming trick: prefix with "a" */
        public string GetCall() => $"I'm a {Sizeness} friend named {Name}!";
        // Implemented interface methods don't require (& can't use) the "override" keyword
    }
    public class LargeCreature : Creature, ISavable // Interfaces must come after inherited classes
    {
        public LargeCreature(string name, int age) : base(name, age, Size.large) {}
        public void Save() => Debug.Log("Saved big buddy");
        // GetCall() is inherited from Creature & thus doesn't need to be stated here explicitly
    }

    public class SmallCreature : Creature, ISavable
    {
        public SmallCreature(string name, int age) : base(name, age, Size.small) {}
        public void Save() => Debug.Log("Saved lil' buddy");
        // GetCall() is inherited
    }

    public void CreatureCalls()
    {
        LargeCreature pygmyHippo = new("Moo Deng", 2);
        LargeCreature giantPanda = new("Yuan Zai", 13);
        SmallCreature greyParrot = new("Apollo G. Bird", 6);
        SmallCreature raccoon = new("Jimothy DeVito", 0);

        StringBuilder buildCalls = new();
        ISavable[] savableObjects = { pygmyHippo, giantPanda, greyParrot, raccoon };
        // Here we're grouping different (in this case, very similar) objects through the ISavable interface
        foreach (ISavable savableObject in savableObjects)
        {
            buildCalls.Append($"{savableObject.GetCall()}\n");
        }
        string creatureCalls = buildCalls.ToString();
        Debug.Log(creatureCalls);
        /* Prints:
        I'm a large friend named Moo Deng!
        I'm a large friend named Yuan Zai!
        I'm a small friend named Apollo G. Bird!
        I'm a small friend named Jimothy DeVito!
        */
        foreach (ISavable savableObject in savableObjects)
        {
            savableObject.Save(); // Save() is defined differently in the 2 child classes, yet ISavable doesn't care
        }
        /* Prints:
        Saved big buddy
        Saved big buddy
        Saved lil' buddy
        Saved lil' buddy
        */
    }



// === Figure out what to do with the following examples
    class ExampleEnumerable : IEnumerable
    {
        private readonly string[] data = new string[] { "one", "two" };
        public IEnumerator GetEnumerator() => data.GetEnumerator();
    }

    IEnumerable<int> BigArray() // === Why not just use for?
    {
        var i = 0;
        while (i < 100)
        {
            yield return i;
            i++;
        }
    }

    IEnumerable<double> SquareEach(IEnumerable<int> data)
    {
        foreach (var item in data)
        {
            var squaredNumber = Mathf.Pow(item, 2);
            yield return squaredNumber;
        }
    }
#endregion Interfaces
#region Unity
// Controls (Windows-specific; look up for your OS)
    /* LMB: left mouse button, RMB: right mouse button, MMB: middle mouse button
    Tools are in the top right of the scene
    You must have the correct tool selected for certain keybinds to work
    // View
    · Zoom: Scroll / Alt+RMB drag
    · Zoom fast: Shft+Alt+RMB drag
    · Zoom to different area: Alt+Scroll
    · Pan view around focus: Alt+LMB
    · Turn view: RMB drag
    · Drag view: MMB drag / Ctrl+Alt+LMB drag
    · Drag view fast: Shft+MMB drag / Ctrl+Shft+Alt+LMB drag
    · Move view: hold RMB & use WASD & Q/E for down/up
        · You may need to drag RMB a little for movement to work
    · Move view fast: RMB+Shft+movement key(s)
    · Control movement speed: RMB+Scroll
    · Focus object: F

    */

    // Remember to save your script each time you want to check changes in Unity!

    public string[] exampleArray1234; /* As a public variable,
    you can add elements to this array within the game object's script component
    In this case, you can select Canvas > TextMeshExample (TMP), go to the Inspector,
    show Intro To C Sharp (Script) > Example Array, & use the plus button to add elements to the array

    However, making every variable public may be dangerous & cause problems,
    so this is a more effective solution: */
    [SerializeField] // This Unity attribute goes directly over the variable to be made visible in the Inspector
    private int SerializedFieldVariable;

    [field: SerializeField] // For properties & other fields, we can use this attribute
    public int SerializedFieldProperty { get; set; }

    void UnityDebugMethods()
    {
        Debug.Log("This is a simple Log Message. A white speech bubble will appear.");
        Debug.LogWarning("This is a Log Warning. A yellow triangle will appear.");
        Debug.LogError("This is a Log Error. A red octagon will appear.");
    }

    #if UNITY_EDITOR
    // This behavior will only run when running the game in the dev environment
    #else
    // This behavior will appear in the actual program
    #endif
#endregion Unity
#region TMPro
    public TextMeshProUGUI textMeshExample;

    [SerializeField]
    private string firstName; // Keeping this unassigned & public means a field will appear in the Unity object
    void DisplayFirstName() // This acts on the Canvas object
    {
        textMeshExample = GetComponent<TextMeshProUGUI>();
        textMeshExample.text = $"Hello, {firstName}!";
        // The dollar sign notation means anything in curly braces is an actual variable
    }
    /* So far, what I've done is added (using the top left of the hierarchy) a UI > Canvas
    & a UI > Text - TextMeshPro. I renamed it TextMeshProExample (TMP)
    I positioned the TMP using the Rect Tool & changed the text color
    Finally, I dragged the script into the TMP object & typed "Douglas" into the First Name field */
    public void CoolButtonAction()
    {
        textMeshExample.text = "You just pressed a button!";
    }
    /* Next, I added a UI > Button - TextMeshPro, renaming it TMPButtonExample
    I positioned it, changed its color, & scaled it 2X & 2Y
    In the Inspector window for the button, under Button > On Click (), I pressed the plus icon
    Then I dragged the TextMeshProExample (TMP) object from the Hierarchy into On Click ()'s object field
    Finally, I changed the function to CSharpTutorial > CoolButtonAction ()
    Try out the button by going to the CSharpTutorial scene, pressing play, & clicking the button
    */
#endregion TMPro
#region LINQ
// === Is the class thing LINQ? What's the difference between it & lambda expression?
    void WordListLengths()
    {
        var words = new[] { "Apple", "Blueberry", "Cherry" };
        var results = words.Select(w => (Word: w, Length: w.Length)); /* "Length" used as int name just to be explicit
        The (=>) notation here basically means we turn one type (string) into a tuple (string, int) */
        foreach (var item in results)
        {
            Debug.Log($"{item.Word} has {item.Length} letters.");
            /* Prints:
            Apple has 5 letters.
            Blueberry has 9 letters.
            Cherry has 6 letters.
            */
        }
    }
#endregion LINQ
}

// Check for ===