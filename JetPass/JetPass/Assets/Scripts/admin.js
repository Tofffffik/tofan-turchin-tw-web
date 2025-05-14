// Modal handling functions
function openModal(modalType) {
    if (modalType === 'add-flight') {
        document.getElementById('add-flight-modal').classList.remove('hidden');
    } else if (modalType === 'edit-flight') {
        document.getElementById('edit-flight-modal').classList.remove('hidden');
    }
}

function closeModal(modalType) {
    if (modalType === 'add-flight') {
        document.getElementById('add-flight-modal').classList.add('hidden');
        document.querySelector('#add-flight-modal form').reset();
    } else if (modalType === 'edit-flight') {
        document.getElementById('edit-flight-modal').classList.add('hidden');
    }
}

// Handle form submissions
document.addEventListener('DOMContentLoaded', function() {
    // Add Flight Form
    const addFlightForm = document.querySelector('#add-flight-modal form');
    addFlightForm.addEventListener('submit', function(e) {
        e.preventDefault();
        createFlight();
    });

    // Edit Flight Form
    const editFlightForm = document.querySelector('#edit-flight-modal form');
    editFlightForm.addEventListener('submit', function(e) {
        e.preventDefault();
        updateFlight();
    });

    // Set up event listeners for edit buttons
    document.querySelectorAll('.flight-row .fa-edit').forEach(button => {
        button.addEventListener('click', function() {
            const flightId = this.closest('tr').dataset.flightId;
            loadFlightData(flightId);
        });
    });

    // Set up event listeners for delete buttons
    document.querySelectorAll('.flight-row .fa-trash-alt').forEach(button => {
        button.addEventListener('click', function() {
            const flightId = this.closest('tr').dataset.flightId;
            if (confirm('Are you sure you want to delete this flight?')) {
                deleteFlight(flightId);
            }
        });
    });
});

// Create a new flight
function createFlight() {
    const form = document.querySelector('#add-flight-modal form');
    const formData = new FormData(form);

    // Convert form data to JSON
    const flightData = {
        AirLine: formData.get('AirLine'),
        AirCraft: formData.get('AirCraft'),
        FlightNumber: formData.get('FlightNumber'),
        DepartureAirport: formData.get('DepartureAirport'),
        ArrivalAirport: formData.get('ArrivalAirport'),
        DepartureTime: formData.get('DepartureTime'),
        ArrivalTime: formData.get('ArrivalTime'),
        FlightDescription: formData.get('FlightDescription'),
        Price: parseFloat(formData.get('Price')),
        Status: formData.get('Status')
    };

    // Handle file upload if needed
    const fileInput = form.querySelector('input[type="file"]');
    if (fileInput.files.length > 0) {
        // In a real app, you would upload the file and get the URL/path
        // For this example, we'll just note that there's an image
        flightData.AirLineImage = true;
    }

    fetch('/Admin/CreateFlight', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(flightData)
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                closeModal('add-flight');
                // Refresh the flight list or add the new flight to the table
                window.location.reload(); // Simple refresh for this example
            } else {
                alert('Error creating flight: ' + data.message);
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('An error occurred while creating the flight');
        });
}

// Load flight data into edit form
function loadFlightData(flightId) {
    fetch(`/Admin/GetFlight?id=${flightId}`)
        .then(response => response.json())
        .then(flight => {
            // Populate the edit form
            const form = document.querySelector('#edit-flight-modal form');
            form.querySelector('input[name="AirLine"]').value = flight.AirLine;
            form.querySelector('input[name="AirCraft"]').value = flight.AirCraft;
            form.querySelector('input[name="FlightNumber"]').value = flight.FlightNumber;
            document.getElementById('edit-flight-number').textContent = flight.FlightNumber;
            form.querySelector('input[name="DepartureAirport"]').value = flight.DepartureAirport;
            form.querySelector('input[name="ArrivalAirport"]').value = flight.ArrivalAirport;

            // Format dates for datetime-local input
            const departureTime = new Date(flight.DepartureTime);
            const arrivalTime = new Date(flight.ArrivalTime);
            form.querySelector('input[name="DepartureTime"]').value = departureTime.toISOString().slice(0, 16);
            form.querySelector('input[name="ArrivalTime"]').value = arrivalTime.toISOString().slice(0, 16);

            form.querySelector('textarea[name="FlightDescription"]').value = flight.FlightDescription;
            form.querySelector('input[name="Price"]').value = flight.Price;
            form.querySelector('select[name="Status"]').value = flight.Status;

            // Handle image if exists
            if (flight.AirLineImage) {
                // In a real app, you would display the current image
                document.getElementById('current-image-preview').classList.remove('hidden');
            }

            // Store flight ID in form for update
            form.dataset.flightId = flight.Id;

            openModal('edit-flight');
        })
        .catch(error => {
            console.error('Error:', error);
            alert('An error occurred while loading flight data');
        });
}

// Update flight
function updateFlight() {
    const form = document.querySelector('#edit-flight-modal form');
    const flightId = form.dataset.flightId;
    const formData = new FormData(form);

    const flightData = {
        Id: parseInt(flightId),
        AirLine: formData.get('AirLine'),
        AirCraft: formData.get('AirCraft'),
        FlightNumber: formData.get('FlightNumber'),
        DepartureAirport: formData.get('DepartureAirport'),
        ArrivalAirport: formData.get('ArrivalAirport'),
        DepartureTime: formData.get('DepartureTime'),
        ArrivalTime: formData.get('ArrivalTime'),
        FlightDescription: formData.get('FlightDescription'),
        Price: parseFloat(formData.get('Price')),
        Status: formData.get('Status')
    };

    // Handle file upload if needed
    const fileInput = form.querySelector('input[type="file"]');
    if (fileInput.files.length > 0) {
        // In a real app, you would upload the file and get the URL/path
        flightData.AirLineImage = true;
    }

    fetch('/Admin/EditFlight', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(flightData)
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                closeModal('edit-flight');
                // Refresh the flight list or update the specific flight in the table
                window.location.reload(); // Simple refresh for this example
            } else {
                alert('Error updating flight: ' + data.message);
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('An error occurred while updating the flight');
        });
}

// Delete flight
function deleteFlight(flightId) {
    fetch('/Admin/DeleteFlight', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ id: flightId })
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                // Remove the flight row from the table or refresh
                document.querySelector(`tr[data-flight-id="${flightId}"]`).remove();
            } else {
                alert('Error deleting flight: ' + data.message);
            }
        })
        .catch(error => {
            console.error('Error:', error);
            alert('An error occurred while deleting the flight');
        });
}

// Image preview functions
function previewImage(input) {
    if (input.files && input.files[0]) {
        const reader = new FileReader();
        reader.onload = function(e) {
            document.getElementById('current-image').src = e.target.result;
            document.getElementById('current-image-preview').classList.remove('hidden');
            document.getElementById('upload-label').textContent = 'Change image';
        }
        reader.readAsDataURL(input.files[0]);
    }
}

function removeImage() {
    const fileInput = document.querySelector('#edit-flight-modal input[type="file"]');
    fileInput.value = '';
    document.getElementById('current-image-preview').classList.add('hidden');
    document.getElementById('upload-label').textContent = 'Upload image';
}