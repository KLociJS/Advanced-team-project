import React, { useState } from 'react'

import {
  CheckMarkIcon,
  Input,
  PrimaryButton,
  XIcon
} from 'Components'

import { isValidUsername, isValidEmail, isValidPassword } from 'Utility';
import { AiOutlineUser } from 'react-icons/ai'




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
    fetch('http://localhost:5000/api/signup', {
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
    <div className='auth-card'>
      <div className='auth-header-container'>
        <AiOutlineUser className='auth-icon'/>
        <h1 className='auth-heading'>Signup</h1>
      </div>
      <Input label={"Username"} inputValue={username} setInputValue={setUsername} setError={setError}/>
      <Input label={"Email"} inputValue={email} setInputValue={setEmail} setError={setError}/>
      <Input label={"Password"} type={"password"} inputValue={password} setInputValue={setPassword} setError={setError}/>
      <PrimaryButton text={"Signup"} clickHandler={handleSubmit} />
      {isRegistrationSuccessful ? (<p className='success'><CheckMarkIcon /> Registration successful</p>) : null}
      {error ? <p className='error'><XIcon/>{error}</p> : null}
    </div>
  );

}