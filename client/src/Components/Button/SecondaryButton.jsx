export default function SecondaryButton({text, clickHandler}) {
  return (
    <button 
        onClick={clickHandler}
        className='secondary-button'
    >
        {text}
    </button>
  )
}
