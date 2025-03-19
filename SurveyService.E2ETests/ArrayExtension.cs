namespace SurveyService.E2ETests;

public static class ArrayExtension
{
    public static T RandomElement<T>(this T[] array)
    {
        var idx = System.Random.Shared.Next(0, array.Length);
        return array[idx];
    }
}