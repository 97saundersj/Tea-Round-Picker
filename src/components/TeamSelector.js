import React, { useState, useCallback } from 'react';
import CreatableSelect from 'react-select/creatable';

const TeamSelector = ({ onTeamSelect }) => {
  // Store teams as objects for react-select
  const [teams, setTeams] = useState([
    { value: 'Team A', label: 'Team A', participants: ['Alice', 'Bob', 'Charlie'] },
    { value: 'Team B', label: 'Team B', participants: ['David', 'Eve', 'Frank'] },
    { value: 'Team C', label: 'Team C', participants: ['George', 'Harry', 'Isabel'] }
  ]);
  
  // Keep track of the currently selected team
  const [selectedTeam, setSelectedTeam] = useState(null);
  const [errorMessage, setErrorMessage] = useState('');

  // Clear error messages on input change
  const handleInputChange = useCallback((newValue) => {
    setErrorMessage('');
    return newValue;
  }, []);

  // When an option is selected, update selectedTeam state and notify the parent
  const handleChange = useCallback((newValue) => {
    setSelectedTeam(newValue);
    onTeamSelect(newValue ? newValue : null);
  }, [onTeamSelect]);

  // When creating a new team, check for duplicates, update the teams list,
  // and automatically select the newly created team
  const handleCreate = useCallback((newValue) => {
    const teamExists = teams.some(team => team.value.toLowerCase() === newValue.toLowerCase());
    if (teamExists) {
      setErrorMessage('This team already exists.');
      return;
    }

    const newTeam = { value: newValue, label: newValue, participants: [] };
    const updatedTeams = [...teams, newTeam];
    setTeams(updatedTeams);
    setSelectedTeam(newTeam);  // Automatically select the new team
    onTeamSelect(newTeam);
  }, [teams, onTeamSelect]);

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
          placeholder="Enter a new team name or choose an existing one..."
        />
        {errorMessage && <div className="text-danger mt-2">{errorMessage}</div>}
      </div>
    </div>
  );
};

export default TeamSelector;
