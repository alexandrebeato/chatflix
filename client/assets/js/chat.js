const { createApp } = Vue;
const apiEndpoint = 'https://localhost:7105/api';

const app = createApp({
  mounted() {
    this.resetForm();
    setInterval(() => {
      if (this.isAuthenticated && this.isChatting) this.getMessages();
    }, 500);
  },
  methods: {
    logout() {
      this.isAuthenticated = false;
      this.user.id = '';
      this.user.token = '';
      this.resetForm();
    },
    resetForm() {
      this.user.userName = 'alexandre.beato';
      this.user.password = '147147';
    },
    signIn() {
      axios({
        method: 'post',
        url: `${apiEndpoint}/users/login`,
        data: {
          userName: this.user.userName,
          password: this.user.password,
        },
      })
        .then((response) => {
          this.isAuthenticated = true;
          this.user.id = response.data.user.id;
          this.user.token = response.data.token;

          this.getRooms();
        })
        .catch((error) => {
          alert('Incorrect credentials');
        });
    },
    signUp() {
      axios({
        method: 'post',
        url: `${apiEndpoint}/users`,
        data: {
          userName: this.user.userName,
          password: this.user.password,
        },
      })
        .then((response) => {
          this.isAuthenticated = true;
          this.user.id = response.data.user.id;
          this.user.token = response.data.token;

          this.getRooms();
        })
        .catch((error) => {
          alert('User already exists');
        });
    },
    leaveRoom() {
      this.room.id = '';
      this.room.roomName = '';
      this.isChatting = false;
    },
    getRooms() {
      axios({
        method: 'get',
        url: `${apiEndpoint}/rooms`,
        headers: {
          Authorization: `Bearer ${this.user.token}`,
        },
      })
        .then((response) => {
          this.rooms = response.data;
        })
        .catch((error) => {
          alert('Error getting rooms');
        });
    },
    createRoom() {
      if (!this.newRoomName) return;

      axios({
        method: 'post',
        url: `${apiEndpoint}/rooms`,
        headers: {
          Authorization: `Bearer ${this.user.token}`,
        },
        data: {
          name: this.newRoomName,
        },
      })
        .then((response) => {
          this.rooms.push(response.data);
          this.newRoomName = '';
        })
        .catch((error) => {
          alert('Error creating room');
        });
    },
    selectRoom(room) {
      this.room.id = room.id;
      this.room.name = room.name;
      this.isChatting = true;
      this.getMessages();
    },
    getMessages() {
      axios({
        method: 'get',
        url: `${apiEndpoint}/messages/room/${this.room.id}`,
        headers: {
          Authorization: `Bearer ${this.user.token}`,
        },
      })
        .then((response) => {
          this.messages = response.data;

          setTimeout(() => {
            var chatHistory = document.getElementById('messages');
            chatHistory.scrollTop = chatHistory.scrollHeight;
          }, 1);
        })
        .catch((error) => {
          alert('Error getting rooms');
        });
    },
    sendMessage() {
      var messageToSend = this.newMessage;
      this.newMessage = '';
      axios({
        method: 'post',
        url: `${apiEndpoint}/messages`,
        headers: {
          Authorization: `Bearer ${this.user.token}`,
        },
        data: {
          roomId: this.room.id,
          content: messageToSend,
        },
      })
        .then((response) => {
          messageToSend = '';
          this.getMessages();
        })
        .catch((error) => {
          alert(`Error sending message: ${error}`);
        });
    },
  },
  data() {
    return {
      applicationMessage: 'Sign in to start chatting',
      isAuthenticated: false,
      isChatting: false,
      room: {
        id: '',
        name: '',
      },
      rooms: [],
      newRoomName: '',
      messages: [],
      user: {
        id: '',
        userName: '',
        password: '',
        token: '',
      },
      newMessage: '',
    };
  },
}).mount('#app');
