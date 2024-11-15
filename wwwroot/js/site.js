document.addEventListener("DOMContentLoaded", function () {
    let selectedAppliancePower = 0;
    const electricityPrices = JSON.parse(document.getElementById('electricityPricesData').textContent);

    function selectAppliance(name, power, element) {
        document.querySelectorAll('.appliance-item').forEach(item => {
            item.classList.remove('selected');
        });

        element.classList.add('selected');
        document.getElementById('selectedAppliance').value = name;
        selectedAppliancePower = power;
        updateTotalCost();
    }

    function updateStartTime() {
        const startTimeSlider = document.getElementById('startTimeSlider');
        const startTimeDisplay = document.getElementById('startTimeDisplay');
        const hour = startTimeSlider.value.padStart(2, '0');
        startTimeDisplay.textContent = `Start Time: ${hour}:00`;

        const startDateTime = new Date();
        startDateTime.setHours(hour, 0, 0, 0);
        document.getElementById('hiddenStartTime').value = startDateTime.toISOString();
        updateTotalCost();
    }

    document.getElementById('hoursSlider').addEventListener('input', function () {
        document.getElementById('hoursValue').textContent = `${this.value} hours`;
        updateTotalCost();
    });

    document.getElementById('minutesSlider').addEventListener('input', function () {
        document.getElementById('minutesValue').textContent = `${this.value} minutes`;
        updateTotalCost();
    });

    function updateTotalCost() {
        const hours = parseInt(document.getElementById('hoursSlider').value);
        const minutes = parseInt(document.getElementById('minutesSlider').value);
        let totalCost = 0;
        let startTime = new Date(document.getElementById('hiddenStartTime').value);

        for (let i = 0; i < hours; i++) {
            const matchingPriceData = electricityPrices.find(p => {
                const priceTime = new Date(p.Time);
                return (
                    priceTime.getHours() === startTime.getHours() &&
                    priceTime.getDate() === startTime.getDate() &&
                    priceTime.getMonth() === startTime.getMonth() &&
                    priceTime.getFullYear() === startTime.getFullYear()
                );
            });

            const pricePerKwh = matchingPriceData ? matchingPriceData.Price : 0.4;
            const hourlyEnergyConsumptionInKwh = selectedAppliancePower / 1000;
            totalCost += hourlyEnergyConsumptionInKwh * pricePerKwh;
            startTime.setHours(startTime.getHours() + 1);
        }

        const priceDataForMinutes = electricityPrices.find(p => {
            const priceTime = new Date(p.Time);
            return (
                priceTime.getHours() === startTime.getHours() &&
                priceTime.getDate() === startTime.getDate() &&
                priceTime.getMonth() === startTime.getMonth() &&
                priceTime.getFullYear() === startTime.getFullYear()
            );
        });

        const pricePerKwhForMinutes = priceDataForMinutes ? priceDataForMinutes.Price : 0.4;
        const energyConsumptionPerMinuteInKwh = (selectedAppliancePower / 1000) / 60;
        totalCost += energyConsumptionPerMinuteInKwh * minutes * pricePerKwhForMinutes;

        document.getElementById('totalCost').textContent = totalCost.toFixed(2);
    }

    window.updateStartTime = updateStartTime;
    window.selectAppliance = selectAppliance;

    updateStartTime();
});
