import React from 'react'
import { IconContext } from 'react-icons'
import { AiOutlineCloseCircle } from 'react-icons/ai'

export default function XIcon() {
  return (
    <IconContext.Provider value={{ color: '#ff0000', size: '1.2rem' }}>
        <AiOutlineCloseCircle/>
    </IconContext.Provider>
  )
}
