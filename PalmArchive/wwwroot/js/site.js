(() => {
  const menu = document.getElementById('sideMenu');
  const overlay = document.getElementById('menuOverlay');
  const openBtn = document.getElementById('menuButton');
  const closeBtn = document.getElementById('closeMenu');
  const setOpen = (open) => {
    if (!menu || !overlay) return;
    menu.classList.toggle('open', open);
    overlay.classList.toggle('open', open);
    menu.setAttribute('aria-hidden', String(!open));
    document.body.style.overflow = open ? 'hidden' : '';
  };
  openBtn?.addEventListener('click', () => setOpen(true));
  closeBtn?.addEventListener('click', () => setOpen(false));
  overlay?.addEventListener('click', () => setOpen(false));
  document.addEventListener('keydown', e => { if (e.key === 'Escape') setOpen(false); });
  const toast = document.querySelector('.toast-message');
  if (toast) setTimeout(() => toast.remove(), 2600);
})();
