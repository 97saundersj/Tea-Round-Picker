import React, { useState, useCallback } from 'react';
import CreatableSelect from 'react-select/creatable';
import { Team } from '../../types/Types';
import { api } from '../../services/api';
import { toast } from 'react-toastify';

export interface TeamSelectorProps {
  onTeamSelect: (team: Team | null) => void;
  teams: Team[];
  fetchTeams: () => Promise<void>;
  isLoading?: boolean;
}

const TeamSelector: React.FC<TeamSelectorProps> = ({ onTeamSelect, teams, fetchTeams, isLoading = false }) => {
  const [selectedTeam, setSelectedTeam] = useState<Team | null>(null);

  // Clear error messages on input change
  const handleInputChange = useCallback((newValue: string) => {
    return newValue;
  }, []);

  // When creating a new team, check for duplicates, update the teams list,
  // and automatically select the newly created team
  const handleCreate = useCallback(async (newValue: string) => {
    if (!newValue) {
      toast.error('Team name cannot be empty.');
      return;
    }

    // Check for duplicates using label
    const teamExists = teams.some(team => team.label?.toLowerCase() === newValue.toLowerCase());
    if (teamExists) {
      toast.error('This team already exists.');
      return;
    }

    const newTeam: Team = { label: newValue, participants: [] };

    try {
      const newCreatedTeam = await api.createTeam(newTeam);
      await fetchTeams();
      setSelectedTeam(newCreatedTeam);
      onTeamSelect(newCreatedTeam);
    } catch (error) {
      console.error('Error creating team:', error);
      toast.error('Error creating team. Please try again.');
    }
  }, [teams, onTeamSelect, fetchTeams]);

  // When an option is selected, update selectedTeam state and notify the parent
  const handleChange = useCallback((newValue: Team | null) => {
    setSelectedTeam(newValue);
    onTeamSelect(newValue);
  }, [onTeamSelect]);

  const handleMenuOpen = useCallback(() => {
    fetchTeams();
  }, [fetchTeams]);

  return (
    <div className="card m-2">
      <div className="card-body">
        <h5 className="card-title">Team</h5>
        <CreatableSelect<Team>
          isClearable
          isSearchable
          isLoading={isLoading}
          options={teams}
          value={selectedTeam}
          onChange={handleChange}
          onInputChange={handleInputChange}
          onCreateOption={handleCreate}
          onMenuOpen={handleMenuOpen}
          getOptionValue={(option) => option?.id?.toString() || ''}
          getOptionLabel={(option) => option?.label || ''}
          placeholder="Enter a new team name or choose an existing one..."
        />
      </div>
    </div>
  );
};

export default TeamSelector; 