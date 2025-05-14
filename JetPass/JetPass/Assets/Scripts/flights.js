document.addEventListener("DOMContentLoaded", function () {
    const searchButton = document.querySelector("button i.fa-search").closest("button");

    async function fetchFlights() {
        const from = document.querySelector('input[placeholder="City or airport"]').value;
        const to = document.querySelectorAll('input[placeholder="City or airport"]')[1].value;
        const fromDate = document.querySelectorAll('input[type="date"]')[0].value;
        const toDate = document.querySelectorAll('input[type="date"]')[1].value;
        const minPrice = document.querySelectorAll('input[type="number"]')[0]?.value || 0;
        const maxPrice = document.querySelectorAll('input[type="number"]')[1]?.value || 99999;

        const filter = {
            From: from,
            To: to,
            FromDate: fromDate || new Date(0).toISOString(),
            ToDate: toDate || new Date(3000, 0, 1).toISOString(),
            SortByPrice: true,
            MinPrice: parseFloat(minPrice),
            MaxPrice: parseFloat(maxPrice),
        };

        try {
            const response = await fetch(`/Flights/GetSortedFlights?${new URLSearchParams(filter)}`);
            const data = await response.json();

            if (data.success) {
                renderFlights(data.flights);
            } else {
                alert("Error loading flights: " + data.message);
            }
        } catch (error) {
            console.error("Error fetching flights:", error);
        }
    }

    function renderFlights(flights) {
        const resultsContainer = document.querySelector(".flex-1 > .search-card");
        const tickets = flights.map(flight => `
            <div class="bg-gray-700 rounded-lg p-6 mb-4 ticket-card">
                <div class="flex flex-col md:flex-row justify-between items-start md:items-center">
                    <div class="flex items-center mb-4 md:mb-0">
                        <img src="https://logo.clearbit.com/${flight.AirLine?.replace(/\s/g, '').toLowerCase()}.com"
                             alt="${flight.AirLine}" class="w-12 h-12 rounded-full mr-4">
                        <div>
                            <h4 class="font-bold">${flight.AirLine}</h4>
                            <p class="text-gray-400 text-sm">${flight.FlightNumber} - ${flight.AirCraft}</p>
                        </div>
                    </div>
                    <div class="flex-1 mx-8 flight-path py-4">
                        <div class="flex justify-between items-center relative z-10">
                            <div class="text-center">
                                <div class="font-bold text-lg">${formatTime(flight.DepartureTime)}</div>
                                <div class="text-gray-400 text-sm">${flight.DepartureAirport}</div>
                            </div>
                            <div class="text-center px-4">
                                <div class="text-gray-400 text-sm">${formatDuration(flight.DepartureTime, flight.ArrivalTime)}</div>
                            </div>
                            <div class="text-center">
                                <div class="font-bold text-lg">${formatTime(flight.ArrivalTime)}</div>
                                <div class="text-gray-400 text-sm">${flight.ArrivalAirport}</div>
                            </div>
                        </div>
                    </div>
                    <div class="flex flex-col items-end">
                        <div class="text-2xl font-bold mb-2">${flight.Price} $</div>
                        <button class="bg-gradient-to-r from-red-500 to-pink-500 text-white font-medium py-2 px-6 rounded-lg">
                            Select
                        </button>
                    </div>
                </div>
            </div>
        `).join("");

        resultsContainer.innerHTML = `
            <div class="flex flex-col md:flex-row justify-between items-center mb-6">
                <h3 class="text-xl font-bold mb-4 md:mb-0">
                    Found ${flights.length} flights
                </h3>
            </div>
            ${tickets}
        `;
    }

    function parseBackendDateTime(dateTimeStr) {
        if (!dateTimeStr) return null;

        // Пробуем стандартный формат
        const parsedDate = new Date(dateTimeStr);
        if (!isNaN(parsedDate.getTime())) return parsedDate;

        // Формат: "5/22/2025 10:46:56 PM"
        const parts = dateTimeStr.trim().split(' ');
        if (parts.length !== 3) return null;

        const [datePart, timePart, period] = parts;
        const [month, day, year] = datePart.split('/').map(Number);
        let [hours, minutes, seconds] = timePart.split(':').map(Number);

        if (period.toUpperCase() === 'PM' && hours < 12) hours += 12;
        if (period.toUpperCase() === 'AM' && hours === 12) hours = 0;

        return new Date(year, month - 1, day, hours, minutes, seconds);
    }

    function formatTime(dateTime) {
        try {
            const date = typeof dateTime === 'string' ? parseBackendDateTime(dateTime) : dateTime;
            if (!date || isNaN(date.getTime())) return '--:--';

            return date.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit', hour12: false });
        } catch (e) {
            console.error('Error formatting time:', e);
            return '--:--';
        }
    }

    function formatDuration(departure, arrival) {
        try {
            const dep = typeof departure === 'string' ? parseBackendDateTime(departure) : departure;
            const arr = typeof arrival === 'string' ? parseBackendDateTime(arrival) : arrival;

            if (!dep || !arr || isNaN(dep.getTime()) || isNaN(arr.getTime())) {
                return '--h --m';
            }

            const diffMs = arr - dep;
            if (diffMs < 0) return '--h --m';

            const hours = Math.floor(diffMs / (1000 * 60 * 60));
            const minutes = Math.floor((diffMs % (1000 * 60 * 60)) / (1000 * 60));
            return `${hours}h ${minutes}m`;
        } catch (e) {
            console.error('Error formatting duration:', e);
            return '--h --m';
        }
    }

    searchButton.addEventListener("click", fetchFlights);
    fetchFlights();
});
