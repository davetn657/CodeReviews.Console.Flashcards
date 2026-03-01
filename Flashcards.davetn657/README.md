# Flashcards Console Application

## Overview

A console based app that uses spaced repetition to promote learning.

Created for the C# Academy based learning

## Requirements

- Users are able to create stacks of flashcards
- Stacks and flashcards tables. Both tables should be linked by a foreign key
- Stacks should have unique names
- Every flashcard must be linked to a stack.
- If a stack is deleted all related flashcards should be deleted
- Use DTOs
- Study Session area, where users can study their stacks
- Stacks and Sessions should have tables and should be linked by a foreign key

### Technologies

- C#
- SQLServer
- Spectre.Console

## Features

Features user friendly user interface allowing navigation through menu options

Launching the application will display a menu with the following options:

Start studying -> Opens a new menu where users will be able to see the past
weeks study sessions and choose which stack to study

Manage data -> Opens a menu where users will be able to create/edit new
study sessions and stacks. Users are able to create new
cards by editing a stack

Exit -> Closes application

## Looking back

### Positives

- When I first started the project it was alot of fun learning new tools.
- Tried to stick to a MVC design, I felt I did well

### Improvements

- Due to poor planning of the UI I ended up refactoring my UI alot
- At the start of the project I had trouble connecting to SQLserver in Visual Studio
