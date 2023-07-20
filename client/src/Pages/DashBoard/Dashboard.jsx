import './DashBoard.css'
import { Link } from 'react-router-dom'

export default function Dashboard() {
  return (
    <div className='menu-card'>
      <h1>Events Dashboard</h1>
      <ul>
        <li>Joined Events: View events you've joined.</li>
        <li>Create Event: Start planning a new event.</li>
        <li>My Events: Manage and edit events you've created.</li>
      </ul>
      <div className='button-container'>
        <Link to='/creat-event' className='remove-underline primary-button'>Create event</Link>
        <Link to='/joined-events' className='remove-underline primary-button'>Joined events</Link>
        <Link to='/created-events' className='remove-underline primary-button'>Created events</Link>
      </div>
      <p>Stay organized and make the most of your event planning experience!</p>
    </div>
  )
}
