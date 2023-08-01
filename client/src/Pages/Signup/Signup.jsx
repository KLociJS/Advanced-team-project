import React, { useState } from 'react'
import {  
  Input, PrimaryButton,
 } from 'Components'


export default function Signup() {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const[isRegistrationSuccessful, setIsRegistrationSuccesful] = useState(false);
  const [error, setError] = useState("");
    
 const handleSubmit = () => {
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
    if(res.ok){
      setIsRegistrationSuccesful(true);
    }
    else{
      throw res;
    }
    return res.json()
  })
  .then(res => console.log(res))
  .catch(err => {
    console.log(err instanceof Response);
    if(err instanceof Response){
      err.json()
      .then(res => setError(res.message[0]));
    }
    else{
      console.error(err);
    }

  });

  
 }
  
  return (
    <div>
      
        <Input label={"Username"} inputValue={username} setInputValue={setUsername}/>
        <Input label={"Email"} inputValue={email} setInputValue={setEmail}/>
        <Input label={"Password"} type={"password"} inputValue={password} setInputValue={setPassword}/>
        <PrimaryButton text={"Register"} clickHandler={handleSubmit} />
        {isRegistrationSuccessful ? (<p>Registration successful</p>) : null}
        {error ? <p>{error}</p> : null}
    </div>    
  );
  
  }