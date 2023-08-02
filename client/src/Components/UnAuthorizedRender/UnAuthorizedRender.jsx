import useAuth from 'Hooks/useAuth'
import React from 'react'

export default function UnAuthorizedRender({ children }) {
    const { user } = useAuth()

    if(user){
        return null
    }

    return (
        <>
        { children }
        </>
    )
}
