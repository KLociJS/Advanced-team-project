import React from 'react'
import { PrimaryButton } from 'Components'
import { useNavigate } from 'react-router-dom'

export default function UpdateButton({ eventId }) {

  const navigate = useNavigate()

  const eventUpdateHandler = (id) => {
    navigate(`/update-event/${id}`)
  }

  return <PrimaryButton text='Update event' clickHandler={()=>eventUpdateHandler(eventId)}/>
}
