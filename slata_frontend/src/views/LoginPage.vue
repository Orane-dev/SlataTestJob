<template>
    <div class="login-container">
        <h2>Sign in</h2>
        <form class="login-form" @submit.prevent="login">

            <p><label>Login: <input class="login-input" type="text" v-model="username" placeholder="Username"
                        required /></label></p>
            <p>
                <lable>Password: <input class="login-input" type="password" v-model="password" placeholder="Password"
                        required /></lable>
            </p>
            <p><label>Department: </label>
                <select class="login-input" v-model="department" required>
                    <option>IT</option>
                    <option>HR</option>
                    <option>Marketing</option>
                    <option>Assortment</option>
                </select>
            </p>
            <button class="login-btn" type="submit">Login</button>
        </form>
    </div>
</template>

<script>
export default {
    data() {
        return {
            username: '',
            password: '',
            department: 'IT'
        };
    },
    methods: {
        login() {
            if (this.username === '123' && this.password === '123') {
                localStorage.setItem('authToken', 'dummy-token');
                localStorage.setItem('department', this.department);
                this.$router.push('/');
            } else {
                alert('Invalid credentials');
            }
        }
    }
};
</script>

<style>
* {
    margin: 0;
    padding: 0;
}

.login-container {
    height: 100vh;
    width: 100%;
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
}

.login-form p {
    margin-top: 15px;
}

.login-btn {
    color: white;
    background: none;
    margin-top: 15px;
    background-color: teal;

}

.login-btn:hover {
    color: teal;
    background-color: white;
}

.login-input,
.login-btn {
    box-sizing: border-box;
    width: 100%;
    padding: 10px;
    border: 2px solid teal;
}
</style>