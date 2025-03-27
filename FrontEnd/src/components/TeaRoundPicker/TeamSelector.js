import React, { useState, useCallback, useEffect } from 'react';
import CreatableSelect from 'react-select/creatable';

const TeamSelector = ({ onTeamSelect }) => {
  // Store teams as objects for react-select
  const [teams, setTeams] = useState([]);
  
  // Keep track of the currently selected team
  const [selectedTeam, setSelectedTeam] = useState(null);
  const [errorMessage, setErrorMessage] = useState('');

  // Fetch teams from the API when the component mounts
  useEffect(() => {
    const fetchTeams = async () => {
      try {
        const response = await fetch(`${process.env.REACT_APP_WEB_API_URL}/teams`);
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        const data = await response.json();
        setTeams(data); // Assuming the API returns an array of team objects
      } catch (error) {
        console.error('Error fetching teams:', error);
      }
    };

    fetchTeams();
  }, []); // Empty dependency array means this runs once on mount

  // Clear error messages on input change
  const handleInputChange = useCallback((newValue) => {
    setErrorMessage('');
    return newValue;
  }, []);

  // When creating a new team, check for duplicates, update the teams list,
  // and automatically select the newly created team
  const handleCreate = useCallback((newValue) => {
    if (!newValue) {
      setErrorMessage('Team name cannot be empty.');
      return;
    }

    // Check for duplicates using id
    const teamExists = teams.some(team => team.label.toLowerCase() === newValue.toLowerCase());
    if (teamExists) {
      setErrorMessage('This team already exists.');
      return;
    }

    // Create a new team with a unique id
    const newTeam = { id: Date.now(), label: newValue, participants: [] }; // Using timestamp as a unique id
    const updatedTeams = [...teams, newTeam];
    setTeams(updatedTeams);
    setSelectedTeam(newTeam);  // Automatically select the new team
    onTeamSelect(newTeam);
  }, [teams, onTeamSelect]);

  // When an option is selected, update selectedTeam state and notify the parent
  const handleChange = useCallback((newValue) => {
    setSelectedTeam(newValue);
    onTeamSelect(newValue ? newValue : null);
  }, [onTeamSelect]);

  return (
    <div className="card m-2">
      <div className="card-body">
        <h5 className="card-title">Team</h5>
        <CreatableSelect
          isClearable
          isSearchable
          options={teams}
          value={selectedTeam}
          onChange={handleChange}
          onInputChange={handleInputChange}
          onCreateOption={handleCreate}
          getOptionValue={(option) => option.id}
          getOptionLabel={(option) => option.label}
          placeholder="Enter a new team name or choose an existing one..."
        />
        {errorMessage && <div className="text-danger mt-2">{errorMessage}</div>}
      </div>
    </div>
  );
};

export default TeamSelector;
