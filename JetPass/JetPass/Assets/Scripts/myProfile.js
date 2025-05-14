function openTab(tabName) {
    // Hide all tab contents
    const tabContents = document.getElementsByClassName('tab-content');
    for (let i = 0; i < tabContents.length; i++) {
        tabContents[i].classList.remove('active');
    }

    // Remove active class from all tab buttons
    const tabButtons = document.getElementsByClassName('tab-button');
    for (let i = 0; i < tabButtons.length; i++) {
        tabButtons[i].classList.remove('active');
        // Update icons color
        const icon = tabButtons[i].querySelector('i');
        icon.classList.remove('text-red-400');
        icon.classList.add('text-gray-400');
    }

    // Show the current tab and mark button as active
    document.getElementById(tabName).classList.add('active');
    const activeButton = document.querySelector(`.tab-button[onclick="openTab('${tabName}')"]`);
    activeButton.classList.add('active');

    // Update icon color for active tab
    const activeIcon = activeButton.querySelector('i');
    activeIcon.classList.remove('text-gray-400');
    activeIcon.classList.add('text-red-400');
}

// Simple form submission handlers
document.addEventListener('DOMContentLoaded', function() {
    // Profile form
    const profileForm = document.querySelector('#profile form');
    profileForm.addEventListener('submit', function(e) {
        e.preventDefault();
        alert('Изменения профиля сохранены!');
    });

    // Delete account button
    const deleteAccountBtn = document.querySelector('#settings button.bg-red-900');
    deleteAccountBtn.addEventListener('click', function() {
        if (confirm('Вы уверены, что хотите удалить аккаунт? Это действие нельзя отменить.')) {
            alert('Аккаунт будет удален. Вы будете перенаправлены на главную страницу.');
            // window.location.href = 'index.html';
        }
    });

    // Cancel booking buttons
    const cancelButtons = document.querySelectorAll('button.bg-red-500');
    cancelButtons.forEach(button => {
        button.addEventListener('click', function() {
            if (confirm('Вы уверены, что хотите отменить бронирование?')) {
                alert('Бронирование отменено. Средства будут возвращены в течение 5-7 дней.');
                // Here you would typically update the UI or reload data
            }
        });
    });

    // Pay booking button
    const payButton = document.querySelector('button.bg-gradient-to-r');
    payButton.addEventListener('click', function() {
        alert('Перенаправление на страницу оплаты...');
        // window.location.href = 'payment.html';
    });
});