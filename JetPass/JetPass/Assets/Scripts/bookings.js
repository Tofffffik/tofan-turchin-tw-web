document.addEventListener('DOMContentLoaded', function() {
    const form = document.querySelector('form');

    form.addEventListener('submit', function(e) {
        e.preventDefault();

        if (validateForm()) {
            this.submit();
        }
    });

    function validateForm() {
        let isValid = true;

        // Reset error states
        document.querySelectorAll('.error-message').forEach(el => el.remove());
        document.querySelectorAll('.border-red-500').forEach(el => el.classList.remove('border-red-500'));

        // Validate each required field
        const requiredFields = [
            'Booking.Name',
            'Booking.LastName',
            'Booking.Citizenship',
            'Booking.PassportNumber',
            'Booking.Email',
            'Booking.Phone'
        ];

        requiredFields.forEach(fieldName => {
            const field = document.querySelector(`[name="${fieldName}"]`);
            if (!field.value.trim()) {
                showError(field, 'This field is required');
                isValid = false;
            }
        });

        // Validate email format
        const emailField = document.querySelector('[name="Booking.Email"]');
        if (emailField.value && !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(emailField.value)) {
            showError(emailField, 'Please enter a valid email address');
            isValid = false;
        }

        return isValid;
    }

    function showError(field, message) {
        field.classList.add('border-red-500');
        const errorElement = document.createElement('div');
        errorElement.className = 'error-message text-red-500 text-sm mt-1';
        errorElement.textContent = message;
        field.parentNode.appendChild(errorElement);
    }
});