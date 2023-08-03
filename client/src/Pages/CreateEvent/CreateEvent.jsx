import { EventForm } from 'Components'


export default function CreateEvent() {

  const postUrl = 'https://localhost:7019/EventEndpoints'

  return (
    <EventForm url={postUrl} navigateTo='/events' />
  )
}
