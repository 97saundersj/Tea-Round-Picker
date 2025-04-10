import React from 'react';
import { render, screen, within } from '@testing-library/react';
import '@testing-library/jest-dom';
import { jest, describe, it, expect, beforeEach } from '@jest/globals';
import TeaRoundPickerPage from '../src/components/TeaRoundPicker/TeaRoundPickerPage';
import userEvent from '@testing-library/user-event';
import { api } from '../src/services/api';
import { Participant, Team } from '../src/types/Types';

jest.mock('../src/services/api');
const mockedApi = jest.mocked(api);

const mockTeams: Team[] = [
  { id: 1, label: 'Team A' },
  { id: 2, label: 'Team B' }
];

const mockParticipants: Participant[] = [
  { id: 1, name: 'John Doe', teamId: 1 },
  { id: 2, name: 'Jane Smith', teamId: 1 }
];

const mockTeamDetails: Team = {
  id: 1,
  label: 'Team A',
  participants: mockParticipants
};

describe('TeaRoundPickerPage Component', () => {
  beforeEach(() => {
    jest.clearAllMocks();
  });

  it('Displays teams when the team selector is clicked on', async () => {
    // Arrange
    mockedApi.getTeams.mockResolvedValue(mockTeams);

    // Act
    render(<TeaRoundPickerPage />);

    const teamSelector = screen.getByTestId('team-selector');
    const selectInput = within(teamSelector).getByRole('combobox');

    await userEvent.click(selectInput);

    // Assert
    expect(mockedApi.getTeams).toHaveBeenCalledTimes(1);
    
    const listbox = within(teamSelector).getByRole('listbox');
    for (const team of mockTeams) {
      expect(within(listbox).getByText(team.label!)).toBeInTheDocument();
    }
  });

  it('displays team details when a team is selected', async () => {
    // Arrange
    mockedApi.getTeams.mockResolvedValue(mockTeams);
    mockedApi.getTeamById.mockResolvedValue(mockTeamDetails);

    // Act
    render(<TeaRoundPickerPage />);
    
    const teamSelector = screen.getByTestId('team-selector');
    const selectInput = within(teamSelector).getByRole('combobox');
    
    await userEvent.click(selectInput);
    const teamOption = within(teamSelector).getByText('Team A');
    await userEvent.click(teamOption);

    // Assert
    expect(mockedApi.getTeamById).toHaveBeenCalledWith(1);
    expect(mockedApi.getTeamById).toHaveBeenCalledTimes(1);

    const participantsList = screen.getByTestId('participants-list');
    expect(participantsList).toBeInTheDocument();

    for (const participant of mockParticipants) {
      const participantsItem = within(participantsList).getByTestId(`participant-item-${participant.id}`);
      expect(within(participantsItem).getByText(participant.name!)).toBeInTheDocument();
    }
  });
  
});
