document.addEventListener('DOMContentLoaded', function() {
    // Десктопное меню пользователя
    const userMenuButton = document.getElementById('user-menu-button');
    const userDropdown = document.getElementById('user-dropdown');

    if (userMenuButton && userDropdown) {
        // Открытие/закрытие dropdown при клике
        userMenuButton.addEventListener('click', function (e) {
            e.stopPropagation();
            const isHidden = userDropdown.classList.contains('hidden');

            if (isHidden) {
                userDropdown.classList.remove('hidden', 'opacity-0', 'scale-95');
                userDropdown.classList.add('opacity-100', 'scale-100');
            } else {
                userDropdown.classList.add('hidden', 'opacity-0', 'scale-95');
                userDropdown.classList.remove('opacity-100', 'scale-100');
            }
        });
        // Закрытие dropdown при клике вне его области
        document.addEventListener('click', function (e) {
            if (!userDropdown.contains(e.target) && e.target !== userMenuButton) {
                userDropdown.classList.add('hidden', 'opacity-0', 'scale-95');
                userDropdown.classList.remove('opacity-100', 'scale-100');
            }
        });
    }

    // Мобильное меню
    const mobileMenuButton = document.getElementById('mobile-menu-button');
    const mobileMenu = document.getElementById('mobile-menu');

    if (mobileMenuButton && mobileMenu) {
        mobileMenuButton.addEventListener('click', function() {
            mobileMenu.classList.toggle('hidden');

            // Меняем иконку при открытии/закрытии
            const icon = mobileMenuButton.querySelector('i');
            if (mobileMenu.classList.contains('hidden')) {
                icon.classList.remove('fa-times');
                icon.classList.add('fa-bars');
            } else {
                icon.classList.remove('fa-bars');
                icon.classList.add('fa-times');
            }
        });
    }
});