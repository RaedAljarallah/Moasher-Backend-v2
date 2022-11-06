/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './Pages/**/*.cshtml',
        './Views/**/*.chstml'
    ],
    theme: {
        extend: {
            fontFamily: {
                cairo: ['Cairo']
            }
        },
    },
    plugins: [
        require('@tailwindcss/forms')({
            strategy: 'class'
        })
    ],
}
