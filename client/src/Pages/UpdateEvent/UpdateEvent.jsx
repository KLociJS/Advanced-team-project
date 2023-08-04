import { EventForm } from 'Components'
import { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'

export default function UpdateEvent() {
    const [event,setEvent] = useState()
    const [isLoading,setIsLoading] = useState(true)
    const { id } = useParams()

    const postUrl = `https://localhost:7019/EventEndpoints${event ? `/${event.id}` : ''}`

    useEffect(()=>{
        fetch(`https://localhost:7019/EventEndpoints/${id}`)
        .then(res=>res.json())
        .then(data=>{
            console.log(data)
            setEvent(data)
            setIsLoading(false)
        })
        .catch(console.log)
    },[id])

    if(isLoading){
        return <div>Loading...</div>
    }

    return (
        <EventForm url={postUrl} navigateTo='/created-events' event={event} buttonText='Update event' method='PUT'/>
    )
}
