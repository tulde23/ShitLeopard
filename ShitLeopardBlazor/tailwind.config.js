const colors = require('tailwindcss/colors');
const plugin = require('tailwindcss/plugin');
module.exports = {
    purge: [],
    darkMode: false, // or 'media' or 'class'
    theme: {
        fontSize: {
            'xs': '.75rem',
            'sm': '.875rem',
            '14': '14px',
            'tiny': '.875rem',
            'base': '1rem',
            'lg': '1.125rem',
            'xl': '1.25rem',
            '2xl': '1.5rem',
            '3xl': '1.875rem',
            '4xl': '2.25rem',
            '5xl': '3rem',
            '6xl': '4rem',
            '7xl': '5rem',
        },
        extend: {
            fontFamily: {
                sans: ['Oswald', 'sans-serif'],
            },
            colors: {
                'stack-blue-300': '#3d5a99',
                'stack-blue-500': '#0a4fb3',
                'stack-blue-700': '#0a4fb3',
                'stack-blue-800': '#0d194f',
                'stack-blue-1': '#0665d9',
                'stack-blue-2': '#0a4fb4',
                'stack-blue-3': '#0f398d',
                'stack-blue-4': '#0d194f',
                'font-blue-1': '#A0CDFE',
            },
        },
    },
    variants: {
        extend: {},
    },
    plugins: [
  
        plugin(function ({ addUtilities, addComponents, e, prefix, config }) {
            // Add your custom styles here
            const newUtilities = {
                '.bg-stack-dark-blue': {
                    'background-color': 'colors.stack-blue-800',
                },
            };

            addUtilities(newUtilities, {
                variants: ['responsive', 'hover'],
            });
        }),
    ],
};
