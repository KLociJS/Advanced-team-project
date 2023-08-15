import useAuth from 'Hooks/useAuth'
import React, { useState } from 'react'
import { Input, PrimaryButton, XIcon } from 'Components'
import { useNavigate } from 'react-router-dom';
import { AiOutlineUser } from 'react-icons/ai'

import './Auth.css'

export default function Login() {
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();
  const { setUser } = useAuth()

  const handleSubmit = () => {
    const userData = {
      userName,
      password
    };

    fetch('https://localhost:7019/api/login', {
      method: "POST",
      credentials: "include",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(userData)
    })
    .then(res => {
      if (!res.ok) {
        return res.json().then(data => {
          throw new Error(data.errorMessage);
        });
      }
      return res.json();
    })
    .then(data => {
      setUser(data)
      console.log(data)
      navigate("/")
    })
    .catch((error) => {
      setError(error.message);
    });
  }

  return (
    <div className='auth-card'>
      <div className='auth-header-container'>
        <AiOutlineUser className='auth-icon'/>
        <h1 className='auth-heading'>Login</h1>
      </div>
      <Input label={"Username"} inputValue={userName} setInputValue={setUserName} setError={setError}/>
      <Input label={"Password"} type={"password"} inputValue={password} setInputValue={setPassword} setError={setError}/>
      <PrimaryButton text={"Login"} clickHandler={handleSubmit} />
      {error ? <p className='error'><XIcon />{error}</p> : null}
    </div>
  )
}
