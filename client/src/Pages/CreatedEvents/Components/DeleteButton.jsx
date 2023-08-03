import PrimaryButton from '../../../Components/Button/PrimaryButton'

export default function DeleteButton({eventId, setEvents}) {
    console.log(eventId)

    const eventDeleteHandler = (id) => {
        fetch(`https://localhost:7019/EventEndpoints/delete-event/${id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include'
        })
        .then(res => {
            if (res.ok) {
                setEvents(events => events.filter(e => e.id !== id))
            }
            return res.json()
        })
        .then(data => {
            console.log(data)
        })
        .catch(console.log)
    }

    return <PrimaryButton text='Delete event' clickHandler={() => eventDeleteHandler(eventId)} />
}
