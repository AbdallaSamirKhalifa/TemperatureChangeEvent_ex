using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class TemperatureChangedEventArgs : EventArgs
{
    public double OldTemprature { get; }

    public double NewTemprature { get; }
    public double Diffrence {  get; }

    public TemperatureChangedEventArgs(double oldTemprature, double newTemprature)
    {
        OldTemprature = oldTemprature;
        NewTemprature = newTemprature;
        Diffrence = NewTemprature-OldTemprature;
    }
}

public class Thermostat
{
    public event EventHandler<TemperatureChangedEventArgs> TempratureChanged;
    public double CurrentTemprature;
    public double OldTemprature;

    public void SetTemptrature(double NewTemprature)
    {
        if (NewTemprature != CurrentTemprature)
        {
            OldTemprature = CurrentTemprature;
            CurrentTemprature = NewTemprature;
            OnTempratureChanged(OldTemprature, CurrentTemprature);
        }
    }

    protected virtual void OnTempratureChanged(TemperatureChangedEventArgs e)
    {
        TempratureChanged?.Invoke(this, e);
    }
    
    public void OnTempratureChanged(double OldTemprature, double CurrentTemprature )
    {  
        OnTempratureChanged(new TemperatureChangedEventArgs(OldTemprature, CurrentTemprature));
    }
}

public class Display
{

    public void Subscribe(Thermostat thermostat)
    {
        thermostat.TempratureChanged += HandleTempratureChange;
    }
    public void HandleTempratureChange(object sensder,TemperatureChangedEventArgs e)
    {
        Console.WriteLine("\n\n  Display Class:");

        Console.WriteLine("\n\n  Temprature Changed:");
        Console.WriteLine($"Temprature Changed From {e.OldTemprature}°C");
        Console.WriteLine($"Temprature Changed To {e.NewTemprature}°C");
        Console.WriteLine($"Temprature Diffrence Is {e.Diffrence}°C");

    }
}

public class Print
{

    public void Subscribe(Thermostat thermostat)
    {
        thermostat.TempratureChanged += HandleTempratureChange;
    }
    public void HandleTempratureChange(object sensder,TemperatureChangedEventArgs e)
    {
        Console.WriteLine("\n\n  Print Class:");
        Console.WriteLine("\n\n  Temprature Changed:");
        Console.WriteLine($"Temprature Changed From {e.OldTemprature}°C");
        Console.WriteLine($"Temprature Changed To {e.NewTemprature}°C");
        Console.WriteLine($"Temprature Diffrence Is {e.Diffrence}°C");

    }
}
class Program
{
    static void Main()
    {
        Thermostat thermostat = new Thermostat();
        Display display = new Display();
        Print print = new Print();
        print.Subscribe(thermostat);
        display.Subscribe(thermostat);

        thermostat.SetTemptrature(25);
        thermostat.SetTemptrature(26.7);

    Console.ReadKey();
    
    
    
    }
}

