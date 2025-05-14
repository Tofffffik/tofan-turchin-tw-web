document.addEventListener('DOMContentLoaded', function() {
    const signInForm = document.getElementById('signInForm');

    if (signInForm) {
        // Add real-time validation on blur
        const emailOrPhone = document.getElementById('EmailOrPhoneNumber');
        if (emailOrPhone) {
            emailOrPhone.addEventListener('blur', validateEmailOrPhone);
        }

        const password = document.getElementById('Password');
        if (password) {
            password.addEventListener('blur', validatePassword);
        }

        // Form submission validation
        signInForm.addEventListener('submit', function(e) {
            if (!validateForm()) {
                e.preventDefault();
            }
        });
    }
});

function validateForm() {
    let isValid = true;

    // Validate Email/Phone
    if (!validateEmailOrPhone()) {
        isValid = false;
    }

    // Validate Password
    if (!validatePassword()) {
        isValid = false;
    }

    return isValid;
}

function validateEmailOrPhone() {
    const emailOrPhone = document.getElementById('EmailOrPhoneNumber');
    const errorElement = document.getElementById('EmailOrPhoneNumber-error');

    if (!emailOrPhone.value.trim()) {
        showError(errorElement, 'Email or phone is required');
        return false;
    } else {
        const value = emailOrPhone.value.trim();
        const isEmail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(value);
        const isPhone = /^\+?[0-9\s\-\(\)]{10,20}$/.test(value);

        if (!isEmail && !isPhone) {
            showError(errorElement, 'Please enter a valid email or phone number');
            return false;
        }
    }

    hideError(errorElement);
    return true;
}

function validatePassword() {
    const password = document.getElementById('Password');
    const errorElement = document.getElementById('Password-error');

    if (!password.value) {
        showError(errorElement, 'Password is required');
        return false;
    } else if (password.value.length < 6) {
        showError(errorElement, 'Password must be at least 6 characters');
        return false;
    }

    hideError(errorElement);
    return true;
}

function showError(element, message) {
    if (element) {
        element.textContent = message;
        element.classList.remove('hidden');
        // Add error styling to input field
        const inputField = element.previousElementSibling;
        if (inputField && inputField.classList.contains('input-field')) {
            inputField.classList.add('border-red-500');
            inputField.classList.remove('border-gray-600');
        }
    }
}

function hideError(element) {
    if (element) {
        element.classList.add('hidden');
        // Remove error styling from input field
        const inputField = element.previousElementSibling;
        if (inputField && inputField.classList.contains('input-field')) {
            inputField.classList.remove('border-red-500');
            inputField.classList.add('border-gray-600');
        }
    }
}