import React, { useState, useCallback } from 'react';
import CreatableSelect from 'react-select/creatable';
import axios from 'axios';
import { Team, CreateTeamDto } from '../../types/Types';

interface TeamSelectorProps {
  onTeamSelect: (team: Team | null) => void;
  teams: Team[];
  fetchTeams: () => Promise<void>;
}

const TeamSelector: React.FC<TeamSelectorProps> = ({ onTeamSelect, teams, fetchTeams }) => {
  const [selectedTeam, setSelectedTeam] = useState<Team | null>(null);
  const [errorMessage, setErrorMessage] = useState<string>('');

  // Clear error messages on input change
  const handleInputChange = useCallback((newValue: string) => {
    setErrorMessage('');
    return newValue;
  }, []);

  // When creating a new team, check for duplicates, update the teams list,
  // and automatically select the newly created team
  const handleCreate = useCallback(async (newValue: string) => {
    if (!newValue) {
      setErrorMessage('Team name cannot be empty.');
      return;
    }

    // Check for duplicates using label
    const teamExists = teams.some(team => team.label?.toLowerCase() === newValue.toLowerCase());
    if (teamExists) {
      setErrorMessage('This team already exists.');
      return;
    }

    // Create a new team object
    const newTeam: CreateTeamDto = { label: newValue };

    try {
      const response = await axios.post<Team>(`${process.env.REACT_APP_WEB_API_URL}/teams`, newTeam, {
        headers: {
          'Content-Type': 'application/json',
        },
      });

      // Check if the response is successful
      if (response.status !== 201) {
        throw new Error('Failed to create team');
      }
      
      await fetchTeams(); // Fetch the updated list of teams
      const newCreatedTeam = response.data;
      setSelectedTeam(newCreatedTeam); // Update selectedTeam state
      onTeamSelect(newCreatedTeam); // Automatically select the newly created team
      
    } catch (error) {
      console.error('Error creating team:', error);
      setErrorMessage('Error creating team. Please try again.');
    }
  }, [teams, onTeamSelect, fetchTeams]);

  // When an option is selected, update selectedTeam state and notify the parent
  const handleChange = useCallback((newValue: Team | null) => {
    setSelectedTeam(newValue);
    onTeamSelect(newValue);
  }, [onTeamSelect]);

  return (
    <div className="card m-2">
      <div className="card-body">
        <h5 className="card-title">Team</h5>
        <CreatableSelect<Team>
          isClearable
          isSearchable
          options={teams}
          value={selectedTeam}
          onChange={handleChange}
          onInputChange={handleInputChange}
          onCreateOption={handleCreate}
          getOptionValue={(option) => option.id.toString()}
          getOptionLabel={(option) => option.label || ''}
          placeholder="Enter a new team name or choose an existing one..."
        />
        {errorMessage && <div className="text-danger mt-2">{errorMessage}</div>}
      </div>
    </div>
  );
};

export default TeamSelector; 