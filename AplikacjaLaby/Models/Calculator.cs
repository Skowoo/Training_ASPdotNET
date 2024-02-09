using static AplikacjaLaby.Controllers.HomeController;

namespace AplikacjaLaby.Models
{
    public class Calculator
    {
        public double? A {  get; set; }

        public double? B { get; set; }

        public Operator? Operation { get; set; }

        public string OperationString
        {
            get
            {
                return Operation switch
                {
                    Operator.Add => "+",
                    Operator.Sub => "-",
                    Operator.Mul => "*",
                    Operator.Div => "/",
                    _ => "?",
                };
            }
        }

        public bool IsValid() => A is not null && B is not null && Operation is not null;

        public double Calculate()
        {
            if (!this.IsValid())
                return double.NaN;

            return Operation switch
            {
                Operator.Add => (double)A! + (double)B!,
                Operator.Sub => (double)A! - (double)B!,
                Operator.Mul => (double)A! * (double)B!,
                Operator.Div => (double)A! / (double)B!,
                _ => double.NaN,
            };
        }

        public Calculator() { }
    }
}
