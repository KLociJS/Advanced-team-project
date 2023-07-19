import React from 'react'
import './Button.css'

export default function PrimaryButton({text, clickHandler}) {
  return (
    <button 
        onClick={clickHandler}
        className='primary-button'
    >
        {text}
    </button>
  )
}
