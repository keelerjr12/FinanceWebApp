class BuyCalculator {

    constructor(residencyLen, investRetRate, housePrice, housePriceGrowthRate,
        downPayment, mortgageTerm, interestRate, propertyTaxRate, insuranceRate,
        maintenanceCostRate, buyingClosingCostRate, sellingClosingCostRate, capGainsRate) {

        this.residencyLen = residencyLen;
        this.investRetRate = investRetRate;
        this.initHousePrice = housePrice;
        this.houseValue = housePrice;
        this.housePriceGrowthRate = housePriceGrowthRate;
        this.downPayment = downPayment;
        this.mortgageTerm = mortgageTerm;
        this.interestRate = interestRate;
        this.propertyTaxRate = propertyTaxRate;
        this.insuranceRate = insuranceRate;
        this.maintenanceCostRate = maintenanceCostRate;
        this.buyingClosingCostRate = buyingClosingCostRate;
        this.sellingClosingCostRate = sellingClosingCostRate;
        this.capGainsRate = capGainsRate;
    }

    calculate() {
        let principal = this.getPrincipal();
        const simLength = Math.min(this.mortgageTerm, this.residencyLen);

        const periodPayment = this.calculatePeriodPayment();

        let recurringCosts = 0;

        let oppCostCumContrib = 0;
        let oppCostInvest = 0;
        const amortizationTable = [];

        for (let period = 1; period <= simLength; period++) {

            const interest = this.interestRate * principal;
            const principalPayment = periodPayment - interest;
            principal -= principalPayment;
            this.houseValue *= (1 + this.housePriceGrowthRate);

            const propertyTax = this.houseValue * this.propertyTaxRate;
            const insurance = this.houseValue * this.insuranceRate;
            const mx = this.houseValue * this.maintenanceCostRate;

            recurringCosts += (periodPayment + propertyTax + insurance + mx);

            const oppCostContrib = (periodPayment + propertyTax + insurance + mx);
            oppCostCumContrib += oppCostContrib;
            oppCostInvest = (1 + this.investRetRate) * oppCostInvest + oppCostContrib;

            const hv = this.houseValue;
            const yearlyData = {
                period,
                periodPayment,
                principalPayment,
                interest,
                principal,
                hv,
                propertyTax,
                insurance,
                mx
            };

            amortizationTable.push(yearlyData);
        }

        const initialCosts = this.downPayment + this.getBuyingClosingCost();

        const invTotal = initialCosts * (Math.pow(1 + this.investRetRate, simLength) - 1);
        const oppCostBeforeTaxes = invTotal + oppCostInvest - oppCostCumContrib;
        const opportunityCosts = (1 - this.capGainsRate) * oppCostBeforeTaxes;

        const netProceeds = this.houseValue - principal - this.getSellingClosingCost();
        const totalCosts = initialCosts + recurringCosts + opportunityCosts - netProceeds;

        return { initialCosts, recurringCosts, opportunityCosts, netProceeds, totalCosts, amortizationTable };
    }

    calculatePeriodPayment() {
        const num = this.interestRate * Math.pow(1 + this.interestRate, this.mortgageTerm);

        const den = Math.pow(1 + this.interestRate, this.mortgageTerm) - 1;
        const periodPayment = this.getPrincipal() * num / den;
        return periodPayment;
    }

    getPrincipal() {
        return this.initHousePrice - this.downPayment;
    }

    getBuyingClosingCost() {
        return this.buyingClosingCostRate * this.initHousePrice;
    }

    getSellingClosingCost() {
        return this.sellingClosingCostRate * this.houseValue;
    }
}

class RentCalculator {
    constructor(residencyLen, investRetRate, securityDepositMonths, insuranceRate, capGainsRate) {
        this.simLength = residencyLen;
        this.investRetRate = investRetRate;
        this.securityDepositMonths = securityDepositMonths;
        this.insuranceRate = insuranceRate;
        this.capGainsRate = capGainsRate;
    }

    calculate(rentPrice) {
        let currRent = rentPrice;

        let recurringCosts = 0;

        let oppCostCumContrib = 0;
        let oppCostInvest = 0;
        const amortizationTable = [];

        for (let period = 1; period <= this.simLength; period++) {
            const insurance = currRent * this.insuranceRate;

            recurringCosts += (currRent + insurance);

            const oppCostContrib = (currRent + insurance);
            oppCostCumContrib += oppCostContrib;
            oppCostInvest = (1 + this.investRetRate) * oppCostInvest + oppCostContrib;
            
            const yearlyData = {
                period,
                currRent,
                insurance
            };

            amortizationTable.push(yearlyData);
        }

        // TODO: RENAME THESE!
        const initialCosts = this.securityDepositMonths * rentPrice;
        const invTotal = initialCosts * (Math.pow(1 + this.investRetRate, this.simLength) - 1);

        const oppCostBeforeTaxes = invTotal + oppCostInvest - oppCostCumContrib;
        const opportunityCosts = (1 - this.capGainsRate) * oppCostBeforeTaxes;

        const netProceeds = this.securityDepositMonths * rentPrice;
        const totalCosts = initialCosts + recurringCosts + opportunityCosts - netProceeds;

        return { initialCosts, recurringCosts, opportunityCosts, netProceeds, totalCosts, amortizationTable };
    }
}