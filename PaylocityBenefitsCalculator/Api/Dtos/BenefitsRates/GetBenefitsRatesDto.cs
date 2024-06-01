namespace Api.Dtos.BenefitsRates;

public class GetBenefitsRatesDto
{
    public int Id { get; set; }
    public int PaychecksPerYear { get; set; }
    public decimal MonthlyBaseCost { get; set; }
    public decimal MonthlyDependentCost { get; set; }
    public decimal HighSalaryLimit { get; set; }
    public decimal HighSalaryCost { get; set; }
    public int OldDependentAgeLimit { get; set; }
    public decimal OldDependentCost { get; set; }
}

