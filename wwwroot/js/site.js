document.addEventListener("DOMContentLoaded", function () {
    let selectedAppliancePower = 0;
    const electricityPrices = JSON.parse(document.getElementById('electricityPricesData').textContent);

    function selectAppliance(name, power) {
        // Ta bort "selected" från alla andra appliance-items
        document.querySelectorAll('.appliance-item').forEach(item => {
            item.classList.remove('selected');
        });

        // Hitta och markera den valda appliance-item
        const selectedElement = Array.from(document.querySelectorAll('.appliance-item')).find(item =>
            item.querySelector('p').textContent === name
        );

        if (selectedElement) {
            selectedElement.classList.add('selected');
        }

        // Sätt vald appliance och uppdatera kostnaden
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

    // Lägg till eventlyssnare för att uppdatera visningen av timmar och minuter
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

    // Exponera funktionerna globalt
    window.updateStartTime = updateStartTime;
    window.selectAppliance = selectAppliance;

    updateStartTime(); // Kör initialt för att sätta starttid och kostnad
});
