import React from 'react';
import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import { jest, describe, it, expect } from '@jest/globals';
import App from '../src/App';

jest.mock('../src/components/TeaRoundPicker/TeaRoundPicker', () => {
  return function DummyTeaRoundPicker() {
    return <div data-testid="mock-tea-round-picker">Tea Round Picker</div>;
  };
});

describe('App Component', () => {
  it('renders without crashing', () => {
    const { container } = render(<App />);

    expect(container).toBeInTheDocument();
  });

  it('renders TeaRoundPicker', () => {
    // Arrange
    render(<App />);
    
    // Act
    const teaRoundPicker = screen.getByTestId('mock-tea-round-picker');

    // Assert
    expect(teaRoundPicker).toBeInTheDocument();
  });
});