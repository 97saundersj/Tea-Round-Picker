import React from 'react';
import { render, screen, within } from '@testing-library/react';
import '@testing-library/jest-dom';
import { jest, describe, it, expect, beforeEach } from '@jest/globals';
import TeaRoundPickerPage from '../src/components/TeaRoundPicker/TeaRoundPickerPage';
import userEvent from '@testing-library/user-event';
import { api } from '../src/services/api';

jest.mock('../src/services/api', () => ({
  api: {
    getTeams: jest.fn(),
    getTeamById: jest.fn(),
    createTeam: jest.fn(),
  },
}));

// 👇 Cast the mocked version of `api`
const mockedApi = api as jest.Mocked<typeof api>;

describe('TeaRoundPickerPage Component', () => {
  beforeEach(() => {
    jest.clearAllMocks();
  });

  it('calls getTeams when the teamSelector is clicked on', async () => {
    // Arrange
    mockedApi.getTeams.mockResolvedValue([]);

    // Act
    render(<TeaRoundPickerPage />);

    const teamSelector = screen.getByTestId('team-selector');
    const selectInput = within(teamSelector).getByRole('combobox');

    await userEvent.click(selectInput);

    // Assert
    expect(mockedApi.getTeams).toHaveBeenCalled();
  });
});
