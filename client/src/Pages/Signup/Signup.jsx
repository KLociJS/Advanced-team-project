import React, { useState } from 'react'
import {
  Input, PrimaryButton,
} from 'Components'
import { isValidUsername, isValidEmail, isValidPassword } from 'Utility';



export default function Signup() {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [isRegistrationSuccessful, setIsRegistrationSuccesful] = useState(false);
  const [error, setError] = useState("");

  const handleSubmit = () => {
    if(!isValidUsername(username)){
      setError("Username has to be at least 4 characters long")
      return
    };
    if(!isValidEmail(email)){
      setError("Please enter a valid email address")
      return
    };
    if(!isValidPassword(password)){
      setError("Password must contain at least one capital letter, one number, and one special character")
      return
    };
    const userData = {
      username,
      email,
      password
    };
    fetch('https://localhost:7019/api/signup', {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(userData)
    })
      .then(res => {
        if (res.ok) {
          setIsRegistrationSuccesful(true);
        }
        else {
          throw res;
        }
        return res.json()
      })
      .then(res => console.log(res))
      .catch(err => {
        console.log(err instanceof Response);
        if (err instanceof Response) {
          err.json()
            .then(res => setError(res.message[0]));
        }
        else {
          console.error(err);
        }

      });


  }

  return (
    <div>

      <Input label={"Username"} inputValue={username} setInputValue={setUsername} setError={setError}/>
      <Input label={"Email"} inputValue={email} setInputValue={setEmail} setError={setError}/>
      <Input label={"Password"} type={"password"} inputValue={password} setInputValue={setPassword} setError={setError}/>
      <PrimaryButton text={"Register"} clickHandler={handleSubmit} />
      {isRegistrationSuccessful ? (<p>Registration successful</p>) : null}
      {error ? <p>{error}</p> : null}
    </div>
  );

}