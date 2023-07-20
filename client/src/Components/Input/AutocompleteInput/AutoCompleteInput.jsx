import { useState } from 'react'

export default function AutoCompleteInput({ inputValue, setInputValue, setId, label, type }) {

    const [suggestions, setSuggestions] = useState([])

    const [isFocused, setIsFocused] = useState(false)

    const handleFocus = () => setIsFocused(true)
    const handleBlur = () => setIsFocused(false)
  
    const isActive = isFocused || inputValue ? 'active' : ''

    const handleInputChange = async(e) => {

        const value = e.target.value
        setInputValue(value)

        if(value.length>3){
            const currSuggestions = await fetchSuggestions()
            setSuggestions(currSuggestions)
        }

        if(suggestions.includes(value)){
            const suggestionId = suggestions.find(s=>s.name).id;
            setId(suggestionId)
        }

    }

    const fetchSuggestions = async (url) =>{
        const suggestionsResponse = await fetch(url)
        return await suggestionsResponse.json()
    }
  
    return (
          <div className={`input-group`}>
              <label className={`input-label ${isActive}`}>
                  {label}
              </label>
              <input
                  type={type || 'text'}
                  value={inputValue}
                  className="input-control"
                  onFocus={handleFocus}
                  onBlur={handleBlur}
                  onChange={handleInputChange}
                  autoComplete="off"
                  list='suggestions'
              />
              <datalist id='suggestions'>
                {suggestions.map(s=>(
                    <option key={s.id} value={s.name}/>
                ))}
              </datalist>
      </div>
    );
}
