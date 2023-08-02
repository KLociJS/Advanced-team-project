import useAuth from 'Hooks/useAuth'
import React from 'react'

export default function Login() {
  const { setUser } = useAuth()
  return (
    <div>Login</div>
  )
}
