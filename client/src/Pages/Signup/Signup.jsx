import React, { useState } from 'react'
import {  
  Input, PrimaryButton,
 } from 'Components'


export default function Signup() {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
    
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
  .then(res => res.json())
  .then(res => console.log(res))
  .catch(err => console.error(err));

  
 }
  
  return (
    <div>
      
        <Input label={"Username"} inputValue={username} setInputValue={setUsername}/>
        <Input label={"Email"} inputValue={email} setInputValue={setEmail}/>
        <Input label={"Password"} type={"password"} inputValue={password} setInputValue={setPassword}/>
        <PrimaryButton text={"Register"} clickHandler={handleSubmit} />
    </div>    
  );
  
  }