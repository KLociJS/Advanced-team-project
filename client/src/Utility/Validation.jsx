


    const isValidUsername = (username) => {
        const usernameRegex = /^[a-zA-Z0-9-._]{4,20}$/;
        return usernameRegex.test(username);
    };

    const isValidEmail = (email) => {
        const emailRegex = /^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$/;
        return emailRegex.test(email);
    };

    const isValidPassword = (password) => {
        const passwordRegex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*\W)(?!.*(.).*\1)[a-zA-Z\d\W]{6,}$/;
        return passwordRegex.test(password);
    }

    export {isValidUsername, isValidEmail, isValidPassword}