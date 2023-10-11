Notification.requestPermission();

const playNotificationSound = () => {
  const audio = new Audio('./assets/sounds/notification.wav');
  audio.play();
};

const showNotification = (title, message) => {
  const notification = new Notification(title, {
    body: message,
    icon: './assets/images/logo.png',
  });
  notification.addEventListener('click', () => {
    window.open('https://google.com');
  });
};

const notify = (title, message) => {
  playNotificationSound();
  showNotification(title, message);
};
