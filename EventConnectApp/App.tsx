// App.tsx
import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import RootNavigator from './app/_layout';

export default function App() {


  return (
    <NavigationContainer>
      <RootNavigator />
    </NavigationContainer>
  );
}
