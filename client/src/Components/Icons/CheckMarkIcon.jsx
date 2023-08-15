import React from 'react'
import { IconContext } from 'react-icons'
import { IoCheckmarkCircleOutline } from 'react-icons/io5'

export default function CheckMarkIcon() {
  return (
    <IconContext.Provider value={{ color: '#008000', size: '1.2rem' }}>
        <IoCheckmarkCircleOutline/>
    </IconContext.Provider>
  )
}
