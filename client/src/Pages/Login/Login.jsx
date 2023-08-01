import useAuth from 'Hooks/useAuth'
import React, { useState } from 'react'
import {  
  Input, PrimaryButton,
 } from 'Components'
import { useNavigate } from 'react-router-dom';

export default function Login() {
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();

  const handleSubmit = () => {
    const userData = {
      userName, 
      password
    };
    console.log(userData);
    fetch('https://localhost:7019/api/login', {
  method: "POST",
  credentials:"include",
  headers: {
    "Content-Type": "application/json" 
  },
  body: JSON.stringify(userData)
  })
  .then(res => {
    if(res.ok){
       return res.json()
    }else{
      throw res;
    }
   
  })
.then(data => {
  console.log(data)
  navigate("/")
})

}
const { setUser } = useAuth()
  return (
    <div>
    <Input label={"Username"} inputValue={userName} setInputValue={setUserName}/>
    <Input label={"Password"} type={"password"} inputValue={password} setInputValue={setPassword}/>
    <PrimaryButton text={"Log In"} clickHandler={handleSubmit} />
    {error ? <p>{error}</p> : null}
</div>    
  )
}
