using InteractiveCLI.Menus;

namespace App.Menus;

public class TopLevelMenu() : Menu(true, true)
{
    protected override void BuildMenu()
    {
        MenuBuilder
            .AddMenuItem<DayOneMenu>("Day One", string.Empty)
            .AddMenuItem<DayTwoMenu>("Day Two", string.Empty)
            .AddMenuItem<DayThreeMenu>("Day Three", string.Empty);
    }
}