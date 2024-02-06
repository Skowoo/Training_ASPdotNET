using System.Runtime.InteropServices.JavaScript;
using Egzamin2023.Services;

namespace Egzamin2023Test;

public class TestDateProvider: IDateProvider
{
    public TestDateProvider(DateTime now)
    {
        _now = now;
    }   
    private DateTime _now;
    public DateTime CurrentDate => _now;
}