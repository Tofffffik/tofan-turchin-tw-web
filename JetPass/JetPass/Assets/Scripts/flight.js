document.addEventListener('DOMContentLoaded', function () {
    const form = document.querySelector('form');
    const nameInput = document.getElementById('name');
    const emailInput = document.getElementById('email');
    const phoneInput = document.getElementById('phone');
    const passwordInput = document.getElementById('password');
    const confirmPasswordInput = document.getElementById('confirm-password');
    const termsCheckbox = document.getElementById('terms');
    const submitButton = form.querySelector('button[type="submit"]');
    const originalButtonText = submitButton.innerHTML;

    function validateEmail(email) {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
    }

    function validatePhone(phone) {
        const phoneRegex = /^\+?[0-9]{10,15}$/;
        const cleaned = phone.replace(/[^\d+]/g, '');
        return phoneRegex.test(cleaned);
    }

    function validatePassword(password) {
        return password.length >= 6 && /\d/.test(password) && /[a-zA-Z]/.test(password);
    }

    function showError(input, message) {
        const group = input.parentElement;
        let error = group.querySelector('.error-message');
        if (!error) {
            error = document.createElement('p');
            error.className = 'error-message text-red-400 text-xs mt-1';
            group.appendChild(error);
        }
        error.textContent = message;
        input.classList.add('border-red-500');
        input.classList.remove('border-gray-600');
    }

    function clearError(input) {
        const group = input.parentElement;
        const error = group.querySelector('.error-message');
        if (error) group.removeChild(error);
        input.classList.remove('border-red-500');
        input.classList.add('border-gray-600');
    }

    function validateForm() {
        let isValid = true;

        const name = nameInput.value.trim();
        const email = emailInput.value.trim();
        const phone = phoneInput.value.trim();
        const password = passwordInput.value;
        const confirmPassword = confirmPasswordInput.value;

        if (!name) {
            showError(nameInput, 'Введите имя');
            isValid = false;
        } else {
            clearError(nameInput);
        }

        if (!email && !phone) {
            showError(emailInput, 'Введите email или телефон');
            showError(phoneInput, 'Введите email или телефон');
            isValid = false;
        } else {
            if (email && !validateEmail(email)) {
                showError(emailInput, 'Некорректный email');
                isValid = false;
            } else {
                clearError(emailInput);
            }

            if (phone && !validatePhone(phone)) {
                showError(phoneInput, 'Некорректный телефон');
                isValid = false;
            } else {
                clearError(phoneInput);
            }
        }

        if (!password) {
            showError(passwordInput, 'Поле обязательно для заполнения');
            isValid = false;
        }
        else if (!validatePassword(password)) {
            showError(passwordInput, 'Неверный пароль. ');
            isValid = false;
        } else {
            clearError(passwordInput);
        }

        if (confirmPassword !== password) {
            showError(confirmPasswordInput, 'Пароли не совпадают');
            isValid = false;
        } else {
            clearError(confirmPasswordInput);
        }

        if (!termsCheckbox.checked) {
            alert('Вы должны согласиться с условиями использования');
            isValid = false;
        }

        return isValid;
    }

    // Удаление ошибок на ввод
    [nameInput, emailInput, phoneInput, passwordInput, confirmPasswordInput].forEach(input => {
        input.addEventListener('input', () => {
            if (input.value.trim()) clearError(input);
        });
    });

    form.addEventListener('submit', function (e) {
        e.preventDefault();

        if (!validateForm()) return;

        const formData = new FormData();
        formData.append('Name', nameInput.value.trim());
        formData.append('Email', emailInput.value.trim());
        formData.append('Phone', phoneInput.value.trim());
        formData.append('Password', passwordInput.value);
        formData.append('ConfirmPassword', confirmPasswordInput.value);

        const token = document.querySelector('input[name="__RequestVerificationToken"]')?.value;
        if (token) {
            formData.append('__RequestVerificationToken', token);
        }

        submitButton.disabled = true;
        submitButton.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Регистрация...';

        fetch('/Account/Register', {
            method: 'POST',
            body: formData,
            headers: {
                'Accept': 'application/json',
                'X-Requested-With': 'XMLHttpRequest'
            }
        })
            .then(response => {
                if (!response.ok) return response.json().then(err => { throw err; });
                return response.json();
            })
            .then(data => {
                if (data.success) {
                    window.location.href = data.redirectUrl || '/';
                } else if (data.errors) {
                    for (const key in data.errors) {
                        const input = document.getElementById(key.toLowerCase());
                        if (input) showError(input, data.errors[key].join(' '));
                    }
                } else if (data.message) {
                    alert(data.message);
                }
            })
            .catch(err => {
                console.error(err);
                alert('Произошла ошибка при регистрации. Попробуйте позже.');
            })
            .finally(() => {
                submitButton.disabled = false;
                submitButton.innerHTML = originalButtonText;
            });
    });
});