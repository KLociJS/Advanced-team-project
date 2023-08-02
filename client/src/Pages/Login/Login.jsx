import useAuth from 'Hooks/useAuth'
import React, { useState } from 'react'
import useAuth from 'Hooks/useAuth'

import {
  Input, PrimaryButton,
} from 'Components'
import { useNavigate } from 'react-router-dom';

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

<<<<<<< HEAD
}
const { setUser } = useAuth()
=======
    console.log(userData);

    fetch('https://localhost:7019/api/login', {
      method: "POST",
      credentials: "include",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(userData)
    })
    .then(res => {
      if (res.ok) {
        return res.json()
      } else {
        throw res;
      }

    })
    .then(data => {
      console.log(data)
      navigate("/")
    })
  }

>>>>>>> dc82f19 (chore: Add auth context)
  return (
    <div>
      <Input label={"Username"} inputValue={userName} setInputValue={setUserName} />
      <Input label={"Password"} type={"password"} inputValue={password} setInputValue={setPassword} />
      <PrimaryButton text={"Log In"} clickHandler={handleSubmit} />
      {error ? <p>{error}</p> : null}
    </div>
  )
}
