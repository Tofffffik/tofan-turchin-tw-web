function validateForm() {
    let isValid = true;

    // Clear previous errors
    document.querySelectorAll('[id$="-error"]').forEach(el => {
        el.classList.add('hidden');
    });

    // Validate Name
    const name = document.getElementById('Name');
    if (!name.value.trim()) {
        showError('Name-error', 'Name is required');
        isValid = false;
    }

    // Validate Email
    const email = document.getElementById('Email');
    if (!email.value.trim()) {
        showError('Email-error', 'Email is required');
        isValid = false;
    } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email.value)) {
        showError('Email-error', 'Please enter a valid email');
        isValid = false;
    }

    // Validate Phone Number
    const phoneNumber = document.getElementById('PhoneNumber');
    if (!phoneNumber.value.trim()) {
        showError('PhoneNumber-error', 'Phone number is required');
        isValid = false;
    } else if (!/^\+?[0-9\s\-\(\)]{10,20}$/.test(phoneNumber.value)) {
        showError('PhoneNumber-error', 'Please enter a valid phone number (10-20 digits, may include +, -, spaces, or parentheses)');
        isValid = false;
    }

    // Validate Password
    const password = document.getElementById('Password');
    if (!password.value) {
        showError('Password-error', 'Password is required');
        isValid = false;
    } else if (password.value.length < 8) {
        showError('Password-error', 'Password must be at least 8 characters');
        isValid = false;
    }

    // Validate Confirm Password
    const confirmPassword = document.getElementById('RepeatPassword');
    if (!confirmPassword.value) {
        showError('RepeatPassword-error', 'Please confirm your password');
        isValid = false;
    } else if (confirmPassword.value !== password.value) {
        showError('RepeatPassword-error', 'Passwords do not match');
        isValid = false;
    }

    // Validate Terms checkbox
    const terms = document.getElementById('AgreeToTermsOfService');
    if (!terms.checked) {
        showError('AgreeToTermsOfService-error', 'You must agree to the terms');
        isValid = false;
    }

    return isValid;
}

function showError(id, message) {
    const errorElement = document.getElementById(id);
    errorElement.textContent = message;
    errorElement.classList.remove('hidden');
}