namespace ProjectManager.Measurements;


public static class TemperatureConverter
{
    public static double CelsiusToFahrenheit(double celsius) => (celsius * 9 / 5) + 32;
    public static double FahrenheitToCelsius(double fahrenheit) => (fahrenheit - 32) * 5 / 9;
    public static double CelsiusToKelvin(double celsius) => celsius + 273.15;
    public static double KelvinToCelsius(double kelvin) => kelvin - 273.15;
    public static double FahrenheitToKelvin(double fahrenheit) => (fahrenheit + 459.67) * 5 / 9;
    public static double KelvinToFahrenheit(double kelvin) => (kelvin * 9 / 5) - 459.67;
}