import { EventForm } from 'Components'


export default function CreateEvent() {

  const postUrl = 'http://localhost:5000/EventEndpoints'

  return (
    <EventForm url={postUrl} navigateTo='/events' buttonText='Create event' method='POST'/>
  )
}
