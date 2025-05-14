document.querySelector('#add-flight-modal form').addEventListener('submit', async function(e) {
    e.preventDefault();

    const formData = new FormData(this);

    // Создаем объект flightEf из данных формы
    const flightEf = {
        AirLine: formData.get('AirLine'),
        AirCraft: formData.get('AirCraft'),
        FlightNumber: formData.get('FlightNumber'),
        DepartureAirport: formData.get('DepartureAirport'),
        ArrivalAirport: formData.get('ArrivalAirport'),
        DepartureTime: new Date(formData.get('DepartureTime')).toISOString(),
        ArrivalTime: new Date(formData.get('ArrivalTime')).toISOString(),
        FlightDescription: formData.get('FlightDescription'),
        Price: parseFloat(formData.get('Price')),
        Status: parseInt(formData.get('Status')), 
        AirLineImage: null
    };

    const imageFile = formData.get('AirLineImage');
    if (imageFile && imageFile.size > 0) {
        const base64String = await readFileAsBase64(imageFile);
        flightEf.AirLineImage = base64String.split(',')[1];
    }

    try {
        // Отправляем данные на сервер
        const response = await fetch('/Flights/CreateFlight', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(flightEf)
        });

        const result = await response.json();

        if (result.success) {
            closeModal('add-flight');
            // Здесь можно обновить список рейсов или выполнить другие действия
        } else {
            alert('Error creating flight: ' + (result.message || 'Unknown error'));
        }
    } catch (error) {
        console.error('Error:', error);
        alert('An error occurred while creating the flight');
    }
});

// Функция для чтения файла как base64
function readFileAsBase64(file) {
    return new Promise((resolve, reject) => {
        const reader = new FileReader();
        reader.onload = () => resolve(reader.result);
        reader.onerror = reject;
        reader.readAsDataURL(file);
    });
}

function closeModal(modalId) {
    document.getElementById(`${modalId}-modal`).classList.add('hidden');
}

function openModal(modalName) {
    document.getElementById(`${modalName}-modal`).classList.remove('hidden');
}